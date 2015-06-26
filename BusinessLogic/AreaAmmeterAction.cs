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
    public class AreaAmmeterAction
    {
        /**
        * 添加台区电表信息
         * @param      ref AreaAmmeterInfo aAInfo：台区电表信息
        * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
        * @author      Rick
        **/
        public int addAAInfo(ref AreaAmmeterInfo aAInfo)
        {

            string strAdd = "insert into AreaAmmeterInfo values (" + aAInfo.AreaNo + "," + aAInfo.AmmeterNo + ",'" + aAInfo.AmmeterName + "'," + aAInfo.AreaAmmeterMulti + ", getdate())";
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
         * 获取台区电表信息
         * @param      string areaNo：台区号
         * @param       ref DataTable dt 存储查询出来信息
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/
        public int getAreaAmmeterByAreaNo(string areaNo, ref DataSet ds)
        {
            string strSelect = "select AmmeterNo, AmmeterName from AreaAmmeterInfo where AreaNo = " + areaNo;
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
        * 获取全部台区电表信息
        * @param       ref DataTable dt 存储查询出来信息
        * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
        * @author      Rick
        **/
        public int getAAInfo(string areaNo, ref DataSet ds)
        {
            string strSelect = "select AmmeterNo, AmmeterName  from AreaAmmeterInfo where AreaNo =" + areaNo;
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
        * 更新台区电表信息
        * @param       string aaNo：台区电表编号
         * @param      ref AreaAmmeterInfo aaInfo：台区信息
        * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
        * @author      Rick
        **/
        public int updateAAInfo(string areaNo, string aaNo, ref AreaAmmeterInfo aaInfo)
        {
            string strUpdate = "update AreaAmmeterInfo set AmmeterName = '" + aaInfo.AmmeterName + "', AreaAmmeterMulti = " + aaInfo.AreaAmmeterMulti + ", AreaAmmeterDate = getdate() where AreaNo = " + areaNo + " and AmmeterNo = " + aaNo;
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
         * 添加新的台区电表信息
         * @param       string aaNo：变压器编号
         * @param       ref AreaAmmeterInfo aaInfo：台区信息
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/
        public int getAAInfoById(string areaNo, string aaNo, ref AreaAmmeterInfo aaInfo)
        {
            string strSelect = "select * from AreaAmmeterInfo where AreaNo = " + areaNo + " and AmmeterNo = " + aaNo;

            try
            {
                DataSet ds = SQLUtl.Query(strSelect);
                DataTable dt = ds.Tables[0];
                if (null != dt.Rows[0].ItemArray[0])
                {
                    aaInfo.AreaNo = dt.Rows[0].ItemArray[0].ToString().Trim();
                }
                else
                {
                    aaInfo.AreaNo = "";
                }

                if (null != dt.Rows[0].ItemArray[1])
                {
                    aaInfo.AmmeterNo = dt.Rows[0].ItemArray[1].ToString().Trim();
                }
                else
                {
                    aaInfo.AmmeterNo = "";
                }

                if (null != dt.Rows[0].ItemArray[2])
                {
                    aaInfo.AmmeterName = dt.Rows[0].ItemArray[2].ToString().Trim();
                }
                else
                {
                    aaInfo.AmmeterName = "";
                }

                if (null != dt.Rows[0].ItemArray[3])
                {
                    aaInfo.AreaAmmeterMulti = dt.Rows[0].ItemArray[3].ToString().Trim();
                }
                else
                {
                    aaInfo.AreaAmmeterMulti = "";
                }

                if (null != dt.Rows[0].ItemArray[4])
                {
                    aaInfo.AreaAmmeterDate = dt.Rows[0].ItemArray[4].ToString().Trim();
                }
                else
                {
                    aaInfo.AreaAmmeterDate = "";
                }


                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
        }


        /**
         * 校验台区电表号是否存在
         * @param       string aaNo：台区电表号
         * @param      ref bool state：false为存在，true为不存在
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/
        public int checkAANo(string areaNo, string aaNo, ref bool state)
        {
            string strCheck = "select count(*) from AreaAmmeterInfo where AreaNo = " + areaNo + " and AmmeterNo = " + aaNo;
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

        public int deleteAAInfoById(string areaNo, string aaNo)
        {
            string deleteSql = "delete from AreaAmmeterInfo where AreaNo = " + areaNo + " and AmmeterNo = " + aaNo;
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
