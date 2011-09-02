# -*- coding: utf-8 -*-
# core.urls
# 

from kay.routing import (
  ViewGroup, Rule
)

view_groups = [
  ViewGroup(
    Rule('/', endpoint='index', view='core.views.index'),
    Rule('/tasks', endpoint='tasks', view='core.views.tasks'),
    Rule('/tasks/<key>', endpoint='task_detail', view='core.views.task_detail'),
  )
]

