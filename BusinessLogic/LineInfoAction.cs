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
    public class LineInfoAction
    {
        /**
         * 获取全部线区信息
         * @param       ref DataSet ds 存储查询出来信息
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/ 
        public int getLineInfo(ref DataSet ds)
        {
            string strSelect = "select LineNum, LineName from LineInfo";
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
         * 根据线区编号获取线区信息
         * @param       ref LineInfo lineInfo 存储查询出来信息
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/
        public int getLineInfoById(string lineNo, ref LineInfo lineInfo)
        {
            string strSql = "select * from LineInfo where LineNum = '" + lineNo + "'";
            try
            {
                DataTable dt = SQLUtl.Query(strSql).Tables[0];
                if (dt.Rows[0].ItemArray[0] != null)
                    lineInfo.LineNum = dt.Rows[0].ItemArray[0].ToString().Trim();
                else
                    lineInfo.LineNum = "";

                if (dt.Rows[0].ItemArray[1] != null)
                    lineInfo.LineName = dt.Rows[0].ItemArray[1].ToString().Trim();
                else
                    lineInfo.LineName = "";

                if (dt.Rows[0].ItemArray[2] != null)
                    lineInfo.PowerPlaceNo = dt.Rows[0].ItemArray[2].ToString().Trim();
                else
                    lineInfo.PowerPlaceNo = "";

                if (dt.Rows[0].ItemArray[3] != null)
                    lineInfo.ReportOrder = dt.Rows[0].ItemArray[3].ToString().Trim();
                else
                    lineInfo.ReportOrder = "";

                if (dt.Rows[0].ItemArray[4] != null)
                    lineInfo.LineDate = dt.Rows[0].ItemArray[4].ToString().Trim();
                else
                    lineInfo.LineDate = "";


                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
        }

        /**
         * 根据线区号更新线区信息
         * @param       string lineNo：要更改的线区的线区号
         * @param       ref LineInfo lineInfo：线区信息
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/ 
        public int updateLineInfo(string lineNo, ref LineInfo lineInfo)
        {
            string strUpdate = "update LineInfo set linename = '" + lineInfo.LineName + "', PowerPlaceNo = " + lineInfo.PowerPlaceNo + ", ReportOrder = " + lineInfo.ReportOrder + ", LineDate = getdate() where LineNum = " + lineInfo.LineNum;
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
         * 添加线区信息
         * @param       ref LineInfo lineInfo：线区信息
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/ 
        public int addLineInfo(ref LineInfo lineInfo)
        {
            string strAdd = "insert into LineInfo values( " + lineInfo.LineNum + ",'" + lineInfo.LineName + "'," +  lineInfo.PowerPlaceNo + ", " + lineInfo.ReportOrder + ", getdate()" + ")";
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

        public int deleteLineInfo(string lineInfoNo)
        {
            string strDelete = "delete from LineInfo where LineNum = " + lineInfoNo;
            try
            {
                SQLUtl.ExecuteSql(strDelete);
                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
        }

        /**
         * 校验线区编码是否唯一
         * @param       string lineNum：线区编码
         * @param       bool state：true为不存在，false为存在
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/ 
        public int checkLineNum(string lineNum, ref bool state)
        {
            string strSql = "select count(*) from LineInfo where LineNum = '" + lineNum + "'";

            try
            {
                DataTable dt = SQLUtl.Query(strSql).Tables[0];
                if (int.Parse(dt.Rows[0].ItemArray[0].ToString().Trim()) > 0)
                    state = false;
                else
                    state = true;
                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
        }
    }
}
