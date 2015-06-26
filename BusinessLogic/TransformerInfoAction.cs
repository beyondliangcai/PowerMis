using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBUtility;
using Common;
using BusinessModel;

namespace BusinessLogic
{
    public class TransformerInfoAction
    {

        /**
       * 添加变压器信息
       * @param       refTransformerLose t_lose：变损信息
       * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
       * @author      Rick
       **/
        public int addTInfo(ref TransformerInfo t_info)
        {

            string strAdd = "insert into TransformerInfo values (" + t_info.TransformerNo + ",'" + t_info.TransformerName + "'," + t_info.TransformerLoseNo + ")";
            try
            {
                SQLUtl.ExecuteSql(strAdd);
                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
        }

        /**
        * 获取全部变压器信息
        * @param       ref DataTable dt 存储查询出来信息
        * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
        * @author      Rick
        **/
        public int getTInfo(ref DataSet ds)
        {
            string strSelect = "select TransformerNo, TransformerName from TransformerInfo";
            try
            {
                ds = SQLUtl.Query(strSelect);
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
            return Constant.OK;
        }

        /**
        * 更新变压器信息
        * @param       string t_info_no：变压器编号
         * @param      ref AreaInfo areaInfo：台区信息
        * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
        * @author      Rick
        **/
        public int updateTInfo(string t_info_no, ref TransformerInfo t_info)
        {
            string strUpdate = "update TransformerInfo set TransformerName = '" + t_info.TransformerName + "', TransformerLoseNo = " + t_info.TransformerLoseNo + " where TransformerNo = " + t_info_no;
            try
            {
                SQLUtl.ExecuteSql(strUpdate);
                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
        }

        /**
         * 添加新变压器信息
         * @param       string t_info_no：变压器编号
         * @param      ref TransformerLose t_lose：台区信息
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/
        public int getTInfoById(string t_info_no, ref TransformerInfo t_info)
        {
            string strSelect = "select * from TransformerInfo where TransformerNo = " + t_info_no;

            try
            {
                DataSet ds = SQLUtl.Query(strSelect);
                DataTable dt = ds.Tables[0];
                if (null != dt.Rows[0].ItemArray[0])
                {
                    t_info.TransformerNo = dt.Rows[0].ItemArray[0].ToString().Trim();
                }
                else
                {
                    t_info.TransformerNo = "";
                }

                if (null != dt.Rows[0].ItemArray[1])
                {
                    t_info.TransformerName = dt.Rows[0].ItemArray[1].ToString().Trim();
                }
                else
                {
                    t_info.TransformerName = "";
                }

                if (null != dt.Rows[0].ItemArray[2])
                {
                    t_info.TransformerLoseNo = dt.Rows[0].ItemArray[2].ToString().Trim();
                }
                else
                {
                    t_info.TransformerLoseNo = "";
                }

                

                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
        }


        /**
         * 校验变压器号是否存在
         * @param       string t_lose_no：台区号
         * @param      ref bool state：false为存在，true为不存在
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/
        public int checkTInfoNo(string t_into_no, ref bool state)
        {
            string strCheck = "select count(*) from TransformerInfo where TransformerNo = " + t_into_no;
            try
            {
                DataSet ds = SQLUtl.Query(strCheck);
                if (int.Parse(ds.Tables[0].Rows[0].ItemArray[0].ToString().Trim()) > 0)
                {
                    state = false;
                }
                else
                {
                    state = true;
                }
                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }

        }

        public int deleteTInfoById(string t_info_no)
        {
            string deleteSql = "delete from TransformerInfo where TransformerNo = " + t_info_no;
            try
            {
                SQLUtl.ExecuteSql(deleteSql);
                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
        }
    }
}
