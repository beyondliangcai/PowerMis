using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BusinessModel;
using Common;
using DBUtility;


namespace BusinessLogic
{
    public class AreaInfoAction
    {
        /**
        * 添加台区信息
        * @param       ref AreaInfo areaInfo：台区信息
        * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
        * @author      Rick
        **/ 
        public int addAreaInfo(ref AreaInfo areaInfo)
        {

            string strAdd = "insert into AreaInfo values (" + areaInfo.AreaNo + ",'" + areaInfo.AreaName + "'," + areaInfo.AreaFlag + ", getdate()," + areaInfo.AreaNo + ")";
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
        * 获取全部台区信息
        * @param       ref DataTable dt 存储查询出来信息
        * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
        * @author      Rick
        **/ 
        public int getAreaInfo(ref DataSet ds)
        {
            string strSelect = "select * from AreaInfo";
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
        * 更新台区信息
        * @param       string areaNo：台区号
         * @param      ref AreaInfo areaInfo：台区信息
        * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
        * @author      Rick
        **/ 
        public int updateAreaInfo(string areaNo, ref AreaInfo areaInfo)
        {
            string strUpdate = "update AreaInfo set AreaName = '" + areaInfo.AreaName + "', AreaFlag = " + areaInfo.AreaFlag + ", AreaDate = getdate() where AreaNo = " + areaNo;
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
         * 添加新台区信息
         * @param       string areaNo：台区号
         * @param      ref AreaInfo areaInfo：台区信息
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/ 
        public int getAreaInfoById(string areaNo, ref AreaInfo areaInfo)
        {
            string strSelect = "select * from AreaInfo where AreaNo = " + areaNo;

            try
            {
                DataSet ds = SQLUtl.Query(strSelect);
                DataTable dt = ds.Tables[0];
                if (null != dt.Rows[0].ItemArray[0])
                {
                    areaInfo.AreaNo = dt.Rows[0].ItemArray[0].ToString().Trim();
                }
                else
                {
                    areaInfo.AreaNo = "";
                }

                if (null != dt.Rows[0].ItemArray[1])
                {
                    areaInfo.AreaName = dt.Rows[0].ItemArray[1].ToString().Trim();
                }
                else
                {
                    areaInfo.AreaName = "";
                }

                if (null != dt.Rows[0].ItemArray[2])
                {
                    areaInfo.AreaFlag = dt.Rows[0].ItemArray[2].ToString().Trim();
                }
                else
                {
                    areaInfo.AreaFlag = "";
                }

                if (null != dt.Rows[0].ItemArray[3])
                {
                    areaInfo.AreaDate = dt.Rows[0].ItemArray[3].ToString().Trim();
                }
                else
                {
                    areaInfo.AreaDate = "";
                }

                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
        }


        /**
         * 校验台区号是否存在
         * @param       string areaNo：台区号
         * @param      ref bool state：false为存在，true为不存在
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/
        public int checkAreaNo(string areaNo, ref bool state)
        {
            string strCheck = "select count(*) from AreaInfo where AreaNo = " + areaNo;
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
            catch(Exception)
            {
                return Constant.ERROR;
            }
           
        }

        public int deleteAreaInfoById(string areaNo)
        {
            string deleteSql = "delete from AreaInfo where AreaNo = " + areaNo;
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
