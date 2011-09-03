# -*- coding: utf-8 -*-

from inspect import (
    isclass
)
from google.appengine.ext import db
from kay.db import OwnerProperty
import models


TYPE_STRINGS = {
    db.Key: "string",
    db.StringProperty: "string",
    db.IntegerProperty: "int",
    db.DateTimeProperty: "System.DateTime",
    db.DateProperty: "System.DateTime",
    db.TimeProperty: "System.DateTime",
    db.TextProperty: "string",
    db.ReferenceProperty: "string",
    db.BooleanProperty: "bool",
    db.FloatProperty: "float",
    db.ListProperty: "System.Collections.List<string>",
    db.StringListProperty: "System.Collections.List<string>",
    db.UserProperty: "string",
    OwnerProperty: "string",
}


def action_generate_models(path="Models.cs"):
    code = ""
    for model in _get_models():
        code += _generate_code(model)
    file = open(path, "w")
    file.write(code)
    file.close()


def _get_models():
    for model_name in dir(models):
        model = models.__dict__[model_name]
        if not isclass(model):
            continue
        if not issubclass(model, db.Model):
            continue
        yield model


def _generate_code(model):
    code = "namespace %s\n" % model.__module__.title()
    code += "{\n"
    
    # データクラスを出力
    code += "    [System.Runtime.Serialization.DataContract]\n"
    code += "    public partial class %s\n" % model.__name__.title()
    code += "    {\n"
    code += "        public Task() { OnCreated(); }\n"
    for name, type in _get_properties(model):
        type_name = TYPE_STRINGS[type]
        code += '        [System.Runtime.Serialization.DataMember(Name="%s")]\n' % name
        code += "        public %s %s { get;set; }\n" %\
                (type_name, name.title())
    code += "        partial void OnCreated();\n"
    code += "    }\n\n"

    # メタクラスを出力
    code += "    public static partial class %sMeta\n" % model.__name__.title()
    code += "    {\n"
    for name, type in _get_properties(model):
        code += '        public static string %s { get { return "%s"; } }\n' %\
                (name.title(), name)
    code += "    }\n"

    code += "}\n\n"
    return code


def _get_properties(model):
    yield "key", db.Key
    for prop_name in dir(model):
        if not prop_name in model.__dict__:
            continue
        prop = model.__dict__[prop_name]
        if not isinstance(prop, db.Property):
            continue
        if not prop.__class__ in TYPE_STRINGS:
            continue
        yield prop_name, prop.__class__



