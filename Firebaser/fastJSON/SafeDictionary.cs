﻿using System.Collections.Generic;

// All the files in fastJSON are copied over from
// https://github.com/mgholam/fastJSON and modified to work under mono 3.5

namespace fastJSON
{
	public sealed class SafeDictionary<TKey, TValue>
	{
		private readonly object _Padlock = new object();
		private readonly Dictionary<TKey, TValue> _Dictionary;

		public SafeDictionary(int capacity)
		{
			_Dictionary = new Dictionary<TKey, TValue>(capacity);
		}

		public SafeDictionary()
		{
			_Dictionary = new Dictionary<TKey, TValue>();
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			lock (_Padlock)
				return _Dictionary.TryGetValue(key, out value);
		}

		public int Count()
		{
			lock (_Padlock) return _Dictionary.Count;
		}

		public TValue this[TKey key]
		{
			get
			{
				lock (_Padlock)
					return _Dictionary[key];
			}
			set
			{
				lock (_Padlock)
					_Dictionary[key] = value;
			}
		}

		public void Add(TKey key, TValue value)
		{
			lock (_Padlock)
			{
				if (_Dictionary.ContainsKey(key) == false)
					_Dictionary.Add(key, value);
			}
		}
	}
}
