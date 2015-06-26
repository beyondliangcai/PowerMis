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
    public class ReceiveAmmeterAction
    {
        /**
      * 添加接收电表信息
      * @param       ref ReceiveAmmeterInfo raInfo：接收电表信息
      * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
      * @author      Rick
      **/
        public int addRaInfo(ref ReceiveAmmeterInfo raInfo)
        {

            string strAdd = "insert into ReceiveInfo values (" + raInfo.ReceiveNo + ",'" + raInfo.ReceiveName + "'," + raInfo.Multipile + ")";
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
        * 获取全部接收电表信息
        * @param       ref DataTable dt 存储查询出来信息
        * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
        * @author      Rick
        **/
        public int getRaInfo(ref DataSet ds)
        {
            string strSelect = "select * from ReceiveInfo";
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
        * 更新接收电表信息
        * @param       string raNo：接收电表编号
         * @param      ref ReceiveAmmeterInfo raInfo：接收电表信息
        * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
        * @author      Rick
        **/
        public int updateRaInfo(string raNo, ref ReceiveAmmeterInfo raInfo)
        {
            string strUpdate = "update ReceiveInfo set ReceiveName = '" + raInfo.ReceiveName + "', Multipile = " + raInfo.Multipile + " where ReceiveNo = " + raNo;
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
         * 添加新接收电表信息
         * @param       string raNo：接收电表编号
         * @param      ref ReceiveAmmeterInfo raInfo：接收电表信息
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/
        public int getRaInfoById(string raNo, ref ReceiveAmmeterInfo raInfo)
        {
            string strSelect = "select * from ReceiveInfo where ReceiveNo = " + raNo;

            try
            {
                DataSet ds = SQLUtl.Query(strSelect);
                DataTable dt = ds.Tables[0];
                if (null != dt.Rows[0].ItemArray[0])
                {
                    raInfo.ReceiveNo = dt.Rows[0].ItemArray[0].ToString().Trim();
                }
                else
                {
                    raInfo.ReceiveNo = "";
                }

                if (null != dt.Rows[0].ItemArray[1])
                {
                    raInfo.ReceiveName = dt.Rows[0].ItemArray[1].ToString().Trim();
                }
                else
                {
                    raInfo.ReceiveName = "";
                }

                if (null != dt.Rows[0].ItemArray[2])
                {
                    raInfo.Multipile = dt.Rows[0].ItemArray[2].ToString().Trim();
                }
                else
                {
                    raInfo.Multipile = "";
                }



                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
        }


        /**
         * 校验接收电表号是否存在
         * @param       string raNo：接受电表号
         * @param      ref bool state：false为存在，true为不存在
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/
        public int checkRaInfoNo(string raNo, ref bool state)
        {
            string strCheck = "select count(*) from ReceiveInfo where ReceiveNo = " + raNo;
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

        public int deleteRaInfoById(string raNo)
        {
            string deleteSql = "delete from ReceiveInfo where ReceiveNo = " + raNo;
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
