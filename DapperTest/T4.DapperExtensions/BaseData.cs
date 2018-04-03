using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using DapperExtensions;
using System.Data.SqlClient;

namespace DapperTest.Data
{
    public partial class BaseData<T> where T : class ,new()
    {

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public dynamic Insert(T model)
        {
            dynamic r = null;
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                cn.Open();
                r = cn.Insert(model);
                cn.Close();
            }

            return r;
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool InsertBatch(List<T> models)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
                {
                    cn.Open();
                    foreach (var model in models)
                    {
                        cn.Insert(model);
                    }
                    cn.Close();
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }



        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(T model)
        {
            dynamic r = null;
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                cn.Open();
                r = cn.Update(model);
                cn.Close();
            }

            return r;
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool UpdateBatch(List<T> models)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
                {
                    cn.Open();
                    foreach (var model in models)
                    {
                        cn.Update(model);
                    }
                    cn.Close();
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        /// <summary>
        ///根据实体删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public dynamic Delete(T model)
        {
            dynamic r = null;
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                cn.Open();
                r = cn.Delete(model);
                cn.Close();
            }

            return r;
        }


        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public dynamic Delete(object predicate)
        {
            dynamic r = null;
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                cn.Open();
                r = cn.Delete(predicate);
                cn.Close();
            }

            return r;
        }

        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool DeleteBatch(List<T> models)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
                {
                    cn.Open();
                    foreach (var model in models)
                    {
                        cn.Delete(model);
                    }
                    cn.Close();
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        /// <summary>
        /// 根据一个实体对象
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public T Get(object id)
        {
            T t = default(T);
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                cn.Open();
                t = cn.Get<T>(id);
                cn.Close();
            }

            return t;

        }


        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public IEnumerable<T> GetList(object predicate = null, IList<ISort> sort = null)
        {
               IEnumerable<T> t = null;
                using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
                {
                    cn.Open();
                    t = cn.GetList<T>(predicate, sort).ToList();//不使用ToList  SqlConnection未初始化
                    cn.Close();
                }

                return t;

        }


        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public int Count(object predicate = null)
        {
            int t = 0;
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                cn.Open();
                t = cn.Count<T>(predicate);
                cn.Close();
            }

            return t;
        }


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="sort">排序</param>
        /// <param name="page">页索引</param>
        /// <param name="resultsPerPage">页大小</param>
        /// <returns></returns>
        public IEnumerable<T> GetPage(object predicate, IList<ISort> sort, int page,int resultsPerPage)
        {
            IEnumerable<T> t = null;
            using (SqlConnection cn = new SqlConnection(DapperHelper.ConnStr))
            {
                cn.Open();
                t = cn.GetPage<T>(predicate, sort, page, resultsPerPage).ToList();
                cn.Close();
            }

            return t;

        }
    }
}