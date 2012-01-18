﻿#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
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
using System;

namespace Bifrost.Configuration.Xml
{
	/// <summary>
	/// Represents an attribute to be used on configuration element classes to let
	/// the element be represented with a different name within the Xml than the classname
	/// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ElementNameAttribute : Attribute
    {
		/// <summary>
		/// Initializes an instance of <see cref="ElementNameAttribute"/>
		/// </summary>
		/// <param name="name">Name to represent the class as</param>
        public ElementNameAttribute(string name)
        {
            Name = name;
        }

		/// <summary>
		/// Get the name that the class should be represented as
		/// </summary>
        public string Name { get; private set; }
    }
}