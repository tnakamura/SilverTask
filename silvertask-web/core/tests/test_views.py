from StringIO import StringIO
from google.appengine.ext import db
from werkzeug import (
    BaseResponse, Client, Request
)
from kay.app import get_application
from kay.utils.test import (
    init_recording, get_last_context, get_last_template,
    disable_recording
)
from kay.ext.testutils.gae_test_base import GAETestBase
from core.models import Task
from core.tests.test_models import TaskTest


class TaskViewTest(GAETestBase):
    CLEANUP_USED_KIND = True

    def setUp(self):
        init_recording()
        app = get_application()
        self.client = Client(app, BaseResponse)

    def tearDown(self):
        disable_recording()

    def test_post(self):
        data = {
            "name": "foo",
        }
        response = self.client.post('/tasks', data=data, follow_redirects=True)
        actual = Task.all().get()
        self.assertEquals("foo", actual.name)

    def test_delete(self):
        key = Task(name="fuga").put()
        tasks = Task.all().fetch(100)
        self.assertEquals(1, len(tasks))

        response = self.client.delete('/tasks/%s' % key, follow_redirects=True)
        tasks = Task.all().fetch(100)
        self.assertEquals(0, len(tasks))

    def test_put(self):
        key = Task(name="hoge").put()
        tasks = Task.all().fetch(100)
        self.assertEquals(1, len(tasks))

        input_stream = StringIO('{ "name" : "fuga" }')
        response = self.client.put('/tasks/%s' % key,
                input_stream=input_stream,
                follow_redirects=True)

        actual = Task.get(key)
        self.assertEquals("fuga", actual.name)

