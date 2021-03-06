﻿#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Bifrost.Events
{
	/// <summary>
	/// Represents arguments for a <see cref="MethodEvent">MethodEvent</see>
	/// </summary>
	public class MethodEventArguments : DynamicObject
	{
		private readonly Dictionary<string, object> _arguments = new Dictionary<string, object>();

		/// <summary>
		/// Gets or sets the value associated with a given argument for a method
		/// </summary>
		/// <param name="argument">Name of argument</param>
		/// <returns>Value for the argument</returns>
		public object this[string argument]
		{
			get { return _arguments[argument]; }
			set { _arguments[argument] = value; }
		}

		/// <summary>
		/// Get all values for all arguments
		/// </summary>
		/// <returns></returns>
		public object[] GetArgumentValues()
		{
			var parameters = _arguments.Values.ToArray();
			return parameters;
		}


#pragma warning disable 1591 // Xml Comments
		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			_arguments[binder.Name] = value;
			return true;
		}

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			result = null;
			if( _arguments.ContainsKey(binder.Name))
			{
				result = _arguments[binder.Name];
				return true;
			}

			return false;
		}
#pragma warning restore 1591 // Xml Comments
	}
}