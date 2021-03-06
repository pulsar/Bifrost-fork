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
using System;
using System.Linq;
using System.Collections.Generic;
using Bifrost.Execution;
using Microsoft.Practices.ServiceLocation;
using System.Reflection;

namespace Bifrost.Commands
{
	/// <summary>
	/// Represents a <see cref="ICommandHandlerInvoker">ICommandHandlerInvoker</see> for handling
	/// command handlers that have methods called Handle() and takes specific <see cref="ICommand">commands</see>
	/// in as parameters
	/// </summary>
	[Singleton]
	public class CommandHandlerInvoker : ICommandHandlerInvoker
	{
        const string HandleMethodName = "Handle";

		readonly ITypeDiscoverer _discoverer;
	    readonly IServiceLocator _serviceLocator;
        readonly Dictionary<Type, MethodInfo> _commandHandlers = new Dictionary<Type, MethodInfo>();
	    bool _initialized;

	    /// <summary>
	    /// Initializes a new instance of <see cref="CommandHandlerInvoker">CommandHandlerInvoker</see>
	    /// </summary>
	    /// <param name="discoverer">A <see cref="ITypeDiscoverer"/> to use for discovering <see cref="ICommandHandler">command handlers</see></param>
	    /// <param name="serviceLocator">A <see cref="IServiceLocator"/> to use for getting instances of objects</param>
	    public CommandHandlerInvoker(ITypeDiscoverer discoverer, IServiceLocator serviceLocator)
		{
			_discoverer = discoverer;
		    _serviceLocator = serviceLocator;
	        _initialized = false;
		}

		private void Initialize()
		{
		    var handlers = _discoverer.FindMultiple<ICommandHandler>();
			Array.ForEach(handlers, Register);
		    _initialized = true;
		}

		/// <summary>
		/// Register a command handler explicitly 
		/// </summary>
		/// <param name="handlerType"></param>
		/// <remarks>
		/// The registration process will look into the handler and find methods that 
		/// are called Handle() and takes a command as parameter
		/// </remarks>
		public void Register(Type handlerType)
		{
            var allMethods = handlerType.GetMethods(BindingFlags.Public | BindingFlags.Instance);

            var query = from m in allMethods
                        where m.Name.Equals(HandleMethodName) &&
                              m.GetParameters().Length == 1 &&
                              typeof(ICommand).IsAssignableFrom(m.GetParameters()[0].ParameterType)
                        select m;

            foreach (var method in query)
                _commandHandlers[method.GetParameters()[0].ParameterType] = method;
		}

#pragma warning disable 1591 // Xml Comments
		public bool TryHandle(ICommand command)
		{
            if( !_initialized)
                Initialize();

            var commandType = command.GetType();
            if (_commandHandlers.ContainsKey(commandType))
            {
                var commandHandlerType = _commandHandlers[commandType].DeclaringType;
                var commandHandler = _serviceLocator.GetInstance(commandHandlerType);
                var method = _commandHandlers[commandType];
                method.Invoke(commandHandler, new[] { command });
                return true;
            }

		    return false;
		}
#pragma warning restore 1591 // Xml Comments
	}
}