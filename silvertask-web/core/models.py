# -*- coding: utf-8 -*-
# core.models

from google.appengine.ext import db
from kay.db import OwnerProperty
from kay.utils import get_or_404


DATETIME_FORMAT = "%Y/%m/%d %H:%M:%S"


class Task(db.Model):
    user = OwnerProperty()
    name = db.StringProperty(required=True)
    done = db.BooleanProperty(default=False)
    created = db.DateTimeProperty(auto_now_add=True)

    def __unicode__(self):
        return self.name

    def to_dict(self):
        user_key = ""
        if self.user:
            user_key = str(self.user.key())
        return {
            "key": str(self.key()),
            "user": user_key,
            "name": self.name,
            "done": self.done,
            "created": self.created.strftime(DATETIME_FORMAT),
        }

    @classmethod
    def read_tasks(cls, user_key, params={}):
        done = cls._get_done_parameter(params)

        return cls.all()\
               .filter("user =", user_key)\
               .filter("done =", done)\
               .order("-created")

    @staticmethod
    def _get_done_parameter(params):
        done = False
        done_string = params.get("done")
        if done_string:
            if done_string == "True":
                done = True
        return done

    @classmethod
    def create_task(cls, data):
        task = cls(name=data["name"])
        key = task.put()
        return cls.get(key)

    @classmethod
    def delete_task(cls, key):
        task = get_or_404(cls, key)
        task.delete()
        return task

    @classmethod
    def update_task(cls, key, data={}):
        task = get_or_404(cls, key)
        for key, val in data.iteritems():
            if hasattr(task, key):
                setattr(task, key, val)
        task.put()
        return task

