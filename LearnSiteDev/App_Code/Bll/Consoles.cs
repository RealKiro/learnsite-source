using System;
using System.Data;
using System.Collections.Generic;
using LearnSite.Common;
using LearnSite.Model;
namespace LearnSite.BLL
{
	/// <summary>
	/// Consoles
	/// </summary>
	public partial class Consoles
	{
		private readonly LearnSite.DAL.Consoles dal=new LearnSite.DAL.Consoles();
		public Consoles()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}
        
        /// <summary>
        /// 标题
        /// </summary>
        /// <param name="Nid"></param>
        /// <returns></returns>
        public string GetTitle(int Nid)
        {
            return dal.GetTitle(Nid);
        }
                
        /// <summary>
        /// 标题
        /// </summary>
        /// <param name="Nid"></param>
        /// <returns></returns>
        public string GetCTitle(int Nid)
        {
            return dal.GetCTitle(Nid);
        }
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Nid)
		{
			return dal.Exists(Nid);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(LearnSite.Model.Consoles model)
		{
			return dal.Add(model);
		}

        public void InitNbegin()
        {
            dal.InitNbegin();
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(LearnSite.Model.Consoles model)
		{
			return dal.Update(model);
		}
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateNbegin(int Nid)
        {
            return dal.UpdateNbegin(Nid);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdatePublish(int Nid)
        {
            return dal.UpdatePublish(Nid);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Nid)
		{
			
			return dal.Delete(Nid);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Nidlist )
		{
			return dal.DeleteList(Nidlist );
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LearnSite.Model.Consoles GetModel(DataTable dt, int Tsort)
        {
            return dal.GetModel(dt,Tsort);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public LearnSite.Model.Consoles GetModel(int Nid)
		{
			
			return dal.GetModel(Nid);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public LearnSite.Model.Consoles GetModelByCache(int Nid)
		{
			
			string CacheKey = "ConsolesModel-" + Nid;
			object objModel = LearnSite.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Nid);
					if (objModel != null)
					{
						int ModelCache = LearnSite.Common.ConfigHelper.GetConfigInt("ModelCache");
						LearnSite.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LearnSite.Model.Consoles)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.Consoles> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<LearnSite.Model.Consoles> DataTableToList(DataTable dt)
		{
			List<LearnSite.Model.Consoles> modelList = new List<LearnSite.Model.Consoles>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				LearnSite.Model.Consoles model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new LearnSite.Model.Consoles();
					if(dt.Rows[n]["Nid"]!=null && dt.Rows[n]["Nid"].ToString()!="")
					{
						model.Nid=int.Parse(dt.Rows[n]["Nid"].ToString());
					}
					if(dt.Rows[n]["Nhid"]!=null && dt.Rows[n]["Nhid"].ToString()!="")
					{
						model.Nhid=int.Parse(dt.Rows[n]["Nhid"].ToString());
					}
					if(dt.Rows[n]["Ncid"]!=null && dt.Rows[n]["Ncid"].ToString()!="")
					{
						model.Ncid=int.Parse(dt.Rows[n]["Ncid"].ToString());
					}
					if(dt.Rows[n]["Ntitle"]!=null && dt.Rows[n]["Ntitle"].ToString()!="")
					{
					model.Ntitle=dt.Rows[n]["Ntitle"].ToString();
					}
					if(dt.Rows[n]["Ncontent"]!=null && dt.Rows[n]["Ncontent"].ToString()!="")
					{
					model.Ncontent=dt.Rows[n]["Ncontent"].ToString();
					}
					if(dt.Rows[n]["Npublish"]!=null && dt.Rows[n]["Npublish"].ToString()!="")
					{
						if((dt.Rows[n]["Npublish"].ToString()=="1")||(dt.Rows[n]["Npublish"].ToString().ToLower()=="true"))
						{
						model.Npublish=true;
						}
						else
						{
							model.Npublish=false;
						}
					}
					if(dt.Rows[n]["Ndate"]!=null && dt.Rows[n]["Ndate"].ToString()!="")
					{
						model.Ndate=DateTime.Parse(dt.Rows[n]["Ndate"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method
	}
}

