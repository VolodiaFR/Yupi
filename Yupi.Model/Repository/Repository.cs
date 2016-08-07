﻿using System;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Yupi.Model.Repository
{
	public class Repository<T> : IRepository<T>
	{
		private ISession session;

		public Repository (ISession session)
		{
			this.session = session;
		}

		public void Save (T entity)
		{
			session.SaveOrUpdate (entity);
		}

		public void Delete (T entity)
		{
			session.Delete (entity);
		}

		public IList<T> ToList ()
		{
			throw new NotImplementedException ();
		}

		public IQueryable<T> All ()
		{
			return session.Query<T>();
		}

		public T FindBy (Expression<Func<T, bool>> expression)
		{
			return FilterBy (expression).Single ();
		}

		public IQueryable<T> FilterBy (Expression<Func<T, bool>> expression)
		{
			return All ().Where (expression).AsQueryable ();
		}

		public T FindBy (int id)
		{
			return session.Get<T> (id);
		}
	}
}
