using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessModel;
using System.Data;
using DBUtility;
using Common;

namespace BusinessLogic
{
    public class AdditionInfoAction
    {
        /**
         * 获取附加信息
         * @param       ref DataSet ds：存储数据集
         * @return      int ：Constant.OK：执行成功；Constant.ERROR：执行失败。
         * @author      Rick;
         **/ 
        public int getAddition(ref DataSet ds)
        {
            string strSql = "select CountryAnnex,EssenceFeeRate from countrycityannexinfo";
            try
            {
                ds = SQLUtl.Query(strSql);
                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
        }

        /**
         * 更新附加信息
         * @param       ref AdditionInfo additionInfo：存储附加信息
         * @return      int ：Constant.OK：执行成功；Constant.ERROR：执行失败。
         * @author      Rick;
         **/
        public int updateAddition(ref AdditionInfo additionInfo)
        {
            string strSql = "update countrycityannexinfo set CountryAnnex = " + additionInfo.CountryAnnex + ", EssenceFeeRate = " + additionInfo.EssenceFeeRate + ", AnnexDate = getdate()";
            try
            {
                 SQLUtl.ExecuteSql(strSql);
                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
        }
    }
}
