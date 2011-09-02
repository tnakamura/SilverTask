# -*- coding: utf-8 -*-

from werkzeug import (
  Response,
)
from kay.utils import (
  render_to_response, url_for,
  render_json_response, get_or_404
)
from kay.auth.decorators import login_required
from core.models import (
    Task
)
import simplejson


@login_required
def index(request):
  return render_to_response('core/index.html', {})


#@login_required
def tasks(request):
    if request.method == "POST":
        task = Task.create_task(request.form)
        return render_json_response(task.to_dict())

    tasks = Task.read_tasks(request.user.key(),
                            params=request.args)
    data = [task.to_dict() for task in tasks]
    return render_json_response(data)


#@login_required
def task_detail(request, key):
    if request.method == "DELETE":
        Task.delete_task(key)
        return Response(key)

    if request.method == "PUT":
        model_dict = simplejson.load(request.stream)
        task = Task.update_task(key, model_dict)
    else:
        task = get_or_404(Task, key)
    return render_json_response(task.to_dict())

