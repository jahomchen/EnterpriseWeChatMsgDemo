using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseWeChatMsgDemo
{
	public class DepartmentResult
	{
		public int Errcode;
		public string Errmsg;
		public List<Department> Department;
	}

	public class Department
	{
		public int Id;
		public string Name;
		public int Parentid;
		public bool CreateDeptGroup;
		public bool AutoAddUser;
	}
}
