from google.appengine.ext import db
from kay.ext.testutils.gae_test_base import GAETestBase
from core.models import Task


class TaskTest(GAETestBase):
    CLEANUP_USED_KIND = True

    def test_to_dict(self):
        t = Task(name="foo")
        key = t.put()

        task = Task.get(key)
        dic = task.to_dict()

        self.assertEquals(str(key), dic["key"])
        self.assertEquals("foo", dic["name"])
        self.assertEquals(False, dic["done"])

    def test_create_task(self):
        data = {
            "name": "foo",
        }
        t = Task.create_task(data)
        self.assertEquals("foo", t.name)

        tasks = Task.all().fetch(100)
        self.assertEquals("foo", tasks[0].name)

    def test_delete_task(self):
        t = Task(name="bar")
        key = t.put()
        tasks = Task.all().fetch(100)
        self.assertEquals(1, len(tasks))

        Task.delete_task(key)
        tasks = Task.all().fetch(100)
        self.assertEquals(0, len(tasks))

    def test_update_task(self):
        t = Task(name="hoge")
        key = t.put()
        tasks = Task.all().fetch(100)
        self.assertEquals(1, len(tasks))

        data = {
            "name": "fuga",
        }
        Task.update_task(key, data)
        actual = Task.all().get()
        self.assertEquals("fuga", actual.name)

