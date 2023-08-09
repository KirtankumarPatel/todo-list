using System.Collection.Generic;

namespace Todo.Models.ViewModels
{
	public class TodoViewModel
	{
		public list<TodoItem> TodoList { get; set; }

		public TodoItem Todo { get; set; }
	}
}