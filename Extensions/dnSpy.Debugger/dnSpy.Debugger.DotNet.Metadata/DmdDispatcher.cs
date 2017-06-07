﻿/*
    Copyright (C) 2014-2017 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;

namespace dnSpy.Debugger.DotNet.Metadata {
	/// <summary>
	/// Dispatches to the owner thread. It's used if the underlying .NET metadata reader is a COM interface
	/// </summary>
	public abstract class DmdDispatcher {
		/// <summary>
		/// Returns true if the current thread is a supported dispatcher thread
		/// </summary>
		/// <returns></returns>
		public abstract bool CheckAccess();

		/// <summary>
		/// Throws if the current thread isn't a dispatcher thread
		/// </summary>
		public void VerifyAccess() {
			if (!CheckAccess())
				throw new InvalidOperationException();
		}

		/// <summary>
		/// Switches to a dispatcher thread and executes <paramref name="callback"/>
		/// </summary>
		/// <param name="callback">Code to execute</param>
		public void Invoke(Action callback) => Invoke<object>(() => { callback(); return null; });

		/// <summary>
		/// Switches to a dispatcher thread and executes <paramref name="callback"/>
		/// </summary>
		/// <typeparam name="T">Type of return data</typeparam>
		/// <param name="callback">Code to execute</param>
		/// <returns></returns>
		public abstract T Invoke<T>(Func<T> callback);
	}
}