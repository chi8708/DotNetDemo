using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using DapperExtensions;
using System.Data.SqlClient;
using DapperTest.Data;

namespace DapperTest.Service
{
    public partial class BaseService<T> where T : class ,new()
    {
        BaseData<T> dal = new BaseData<T>();
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public dynamic Insert(T model)
        {
            return dal.Insert(model);
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool InsertBatch(List<T> models)
        {
            return dal.InsertBatch(models);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(T model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool UpdateBatch(List<T> models)
        {
            return dal.UpdateBatch(models);
        }


        /// <summary>
        ///根据实体删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public dynamic Delete(T model)
        {
            return dal.Delete(model);
        }


        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public dynamic Delete(object predicate)
        {
            return dal.Delete(predicate);
        }

        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool DeleteBatch(List<T> models)
        {
            return dal.DeleteBatch(models);
        }


        /// <summary>
        /// 根据一个实体对象
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public T Get(object id)
        {
            return dal.Get(id);
        }


        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public IEnumerable<T> GetList(object predicate = null, IList<ISort> sort = null)
        {
            return dal.GetList(predicate,sort);

        }

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public int Count(object predicate = null)
        {
            return dal.Count(predicate);
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
            return dal.GetPage(predicate, sort, page, resultsPerPage);

        }
    }
}