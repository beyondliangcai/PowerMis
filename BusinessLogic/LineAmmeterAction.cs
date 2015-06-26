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
    public class LineAmmeterAction
    {
        /**
         * 添加线路电表信息
         * @param       ref LineAmmeterInfo lineAmmeterInfo：线路电表信息
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/
        public int addLineAmmeter(ref LineAmmeterInfo lineAmmeterInfo)
        {

            string strAdd = "insert into LineAmmeterInfo values (" + lineAmmeterInfo.LineNum + "," + lineAmmeterInfo.AmmeterNo + ", '" + lineAmmeterInfo.AmmeterName + "', " + lineAmmeterInfo.LineAmmeterMulti +", getdate())";
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
         * 获取线路电表信息
         * @param      string lineNo：线路号
         * @param       ref DataTable dt 存储查询出来信息
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/
        public int getLineAmmeterByLineNo(string lineNo, ref DataSet ds)
        {
            string strSelect = "select AmmeterNo, AmmeterName from LineAmmeterInfo where LineNum = " + lineNo;
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
        * 更新线路电表信息
        * @param       ref LineAmmeterInfo lineAmmeterInfo：电表信息
         * @param      string l_a_no：电表号
        * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
        * @author      Rick
        **/
        public int updateLineAmmeter(string lineNo, string l_a_no,  ref LineAmmeterInfo lineAmmeterInfo)
        {
            string strUpdate = "update LineAmmeterInfo set AmmeterName = '" + lineAmmeterInfo.AmmeterName + "', LineAmmeterMulti = " + lineAmmeterInfo.LineAmmeterMulti + ", LineAmmeterDate = getdate()" + " where LineNum = " + lineNo + "and AmmeterNo = " + l_a_no;
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
         * 查询线路电表信息
         * @param       string lineNo：线路号
         * @param       string l_a_no：线路电表编号
         * @param       ref LineAmmeterInfo lineAmmeterInfo：线路电表信息
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/
        public int getLineAmmeterById(string lineNo, string l_a_no, ref LineAmmeterInfo lineAmmeterInfo)
        {
            string strSelect = "select * from LineAmmeterInfo where  LineNum = " + lineNo + " and AmmeterNo = " + l_a_no;

            try
            {
                DataSet ds = SQLUtl.Query(strSelect);
                DataTable dt = ds.Tables[0];
                if (null != dt.Rows[0].ItemArray[0])
                {
                    lineAmmeterInfo.LineNum = dt.Rows[0].ItemArray[0].ToString().Trim();
                }
                else
                {
                    lineAmmeterInfo.LineNum = "";
                }

                if (null != dt.Rows[0].ItemArray[1])
                {
                    lineAmmeterInfo.AmmeterNo = dt.Rows[0].ItemArray[1].ToString().Trim();
                }
                else
                {
                    lineAmmeterInfo.AmmeterNo = "";
                }

                if (null != dt.Rows[0].ItemArray[2])
                {
                    lineAmmeterInfo.AmmeterName = dt.Rows[0].ItemArray[2].ToString().Trim();
                }
                else
                {
                    lineAmmeterInfo.AmmeterName = "";
                }

                if (null != dt.Rows[0].ItemArray[3])
                {
                    lineAmmeterInfo.LineAmmeterMulti = dt.Rows[0].ItemArray[3].ToString().Trim();
                }
                else
                {
                    lineAmmeterInfo.LineAmmeterMulti = "";
                }

                if (null != dt.Rows[0].ItemArray[4])
                {
                    lineAmmeterInfo.LineAmmeterDate = dt.Rows[0].ItemArray[4].ToString().Trim();
                }
                else
                {
                    lineAmmeterInfo.LineAmmeterDate = "";
                }



                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
        }


        /**
         * 校验电表号是否存在
         * @param       string l_a_no：线路电表号
         * @param       ref bool state：false为存在，true为不存在
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/
        public int checkLANo(string lineNo,string l_a_no, ref bool state)
        {
            string strCheck = "select count(*) from LineAmmeterInfo where LineNum = " + lineNo + "and AmmeterNo = " + l_a_no;
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

        public int deleteLANoById(string lineNo, string l_a_no)
        {
            string deleteSql = "delete from LineAmmeterInfo where LineNum = " + lineNo + "and AmmeterNo = " + l_a_no;
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
