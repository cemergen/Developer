﻿using Developer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developer.Data.Repo
{
   
        public class UnitOfWork
        {
            private readonly DevContext context;
            private bool disposed;
            private Dictionary<string, object> repositories;

            public UnitOfWork(DevContext context)
            {
                this.context = context;
            }

            public UnitOfWork()
            {
                context = new DevContext();
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            public void Save()
            {
                context.SaveChanges();
            }

            public virtual void Dispose(bool disposing)
            {
                if (!disposed)
                {
                    if (disposing)
                    {
                        context.Dispose();
                    }
                }
                disposed = true;
            }

            public Repository<T> Repository<T>() where T : BaseEntity
            {
                if (repositories == null)
                {
                    repositories = new Dictionary<string, object>();
                }

                var type = typeof(T).Name;

                if (!repositories.ContainsKey(type))
                {
                    var repositoryType = typeof(Repository<>);
                    var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), context);
                    repositories.Add(type, repositoryInstance);
                }
                return (Repository<T>)repositories[type];
            }
        }
    }

