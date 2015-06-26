using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBUtility;
using Common;
using BusinessModel;
//using BusinessLogic;

namespace BusinessLogic
{
    public class TransformerLoseAction
    {
        /**
       * 添加变损信息
       * @param       ref List<TransformerLoseInfo> t_lose：变损信息
       * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
       * @author      Rick
       **/
        public int addTLose(ref List<TransformerLose> t_lose)
        {

            //string strAdd = "insert into AreaInfo values (" + areaInfo.AreaNo + ",'" + areaInfo.AreaName + "'," + areaInfo.AreaFlag + ", getdate())";
            int count = t_lose.Count;
            int i = 0;
            List<string> listSql = new List<string>();

            
            while (count > 0)
            {
                string strAdd = "insert into TransformerLoseInfo values (" + t_lose[i].TransformerLoseNo + ", '" + t_lose[i].TransformerType + "'," + t_lose[i].StandarVolume + ", " + t_lose[i].MonthUsed + "," + t_lose[i].TranformerLose + "," + t_lose[i].LessOrMoreFlag + ")";
                listSql.Add(strAdd);

                count = count - 1;
                i = i + 1;
            }
            
            
            try
            {
                SQLUtl.ExecuteSqlTran(listSql);
                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
        }

        /**
        * 获取全部变损信息
        * @param       ref DataTable dt 存储查询出来信息
        * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
        * @author      Rick
        **/
        public int getTLose(ref DataSet ds)
        {
            string strSelect = "select  distinct TransformerLoseNo, TransformerType, standarVolume from TransformerLoseInfo order by TransformerLoseNo";
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
        * 根据ID获取变损类型
        * @param       ref DataTable dt 存储查询出来信息
        * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
        * @author      Rick
        **/
        public int getTLoseTypeById(string TLoseNo, ref TransformerLose t_lose)
        {
            string strSelect = "select  distinct TransformerLoseNo, TransformerType, standarVolume from TransformerLoseInfo where TransformerLoseNo = " + TLoseNo;
            try
            {
                DataTable dt = SQLUtl.Query(strSelect).Tables[0];
                if (null != dt.Rows[0].ItemArray[0])
                {
                    t_lose.TransformerLoseNo = dt.Rows[0].ItemArray[0].ToString().Trim();
                }
                else
                {
                    t_lose.TransformerLoseNo = "";
                }

                if (null != dt.Rows[0].ItemArray[1])
                {
                    t_lose.TransformerType = dt.Rows[0].ItemArray[1].ToString().Trim();
                }
                else
                {
                    t_lose.TransformerType = "";
                }

                if (null != dt.Rows[0].ItemArray[2])
                {
                    t_lose.StandarVolume = dt.Rows[0].ItemArray[2].ToString().Trim();
                }
                else
                {
                    t_lose.StandarVolume = "";
                }

                return Constant.OK;
            }catch(Exception)
            {
                return Constant.ERROR;
            }
        }
        /**
        * 更新变损信息
        * @param       string t_lose_no：变损号
         * @param      ref List<TransformerLose> t_lose_list：变损信息
        * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
        * @author      Rick
        **/
        public int updateTLose(string t_lose_no, ref List<TransformerLose> t_lose_list)
        {
            //string strUpdate = "update AreaInfo set AreaName = '" + areaInfo.AreaName + "', AreaFlag = " + areaInfo.AreaFlag + ", AreaDate = getdate() where AreaNo = " + areaNo;
            List<string> listSql = new List<string>();
            int count = t_lose_list.Count;
            int i = 0;


            if (count > 0)
            {
                string strDelete = "delete from TransformerLoseInfo where TransformerLoseNo = " + t_lose_no;
                listSql.Add(strDelete);

                while (count > 0)
                {
                    string strInsert = "insert into TransformerLoseInfo values( " + t_lose_list[i].TransformerLoseNo + ",'" + t_lose_list[i].TransformerType + "'," + t_lose_list[i].StandarVolume + "," + t_lose_list[i].MonthUsed + "," + t_lose_list[i].TranformerLose + "," + t_lose_list[i].LessOrMoreFlag + ")";
                    listSql.Add(strInsert);
                    count = count - 1;
                    i = i + 1;
                }
            }
            
            try
            {
                SQLUtl.ExecuteSqlTran(listSql);
                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
        }

        /**
         * 添加新变损信息
         * @param       string t_no：变损编号
         * @param      ref List<TransformerLose> list：变损信息
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/
        public int getTLoseById(string t_lose_no, ref List<TransformerLose> list)
        {
            string strSelect = "select * from TransformerLoseInfo where TransformerLoseNo = " + t_lose_no;

            try
            {
                DataSet ds = SQLUtl.Query(strSelect);
                DataTable dt = ds.Tables[0];
                int count = dt.Rows.Count;
                int i = 0;
                while (count > 0)
                {
                    TransformerLose t_lose = new TransformerLose();


                    //给对象t_lose_no赋值
                    if (null != dt.Rows[i].ItemArray[0])
                    {
                        t_lose.TransformerLoseNo = dt.Rows[i].ItemArray[0].ToString().Trim();
                    }
                    else
                    {
                        t_lose.TransformerLoseNo = "";
                    }

                    if (null != dt.Rows[i].ItemArray[1])
                    {
                        t_lose.TransformerType = dt.Rows[i].ItemArray[1].ToString().Trim();
                    }
                    else
                    {
                        t_lose.TransformerType = "";
                    }

                    if (null != dt.Rows[i].ItemArray[2])
                    {
                        t_lose.StandarVolume = dt.Rows[i].ItemArray[2].ToString().Trim();
                    }
                    else
                    {
                        t_lose.StandarVolume = "";
                    }

                    if (null != dt.Rows[i].ItemArray[3])
                    {
                        t_lose.MonthUsed = dt.Rows[i].ItemArray[3].ToString().Trim();
                    }
                    else
                    {
                        t_lose.MonthUsed = "";
                    }

                    if (null != dt.Rows[i].ItemArray[4])
                    {
                        t_lose.TranformerLose = dt.Rows[i].ItemArray[4].ToString().Trim();
                    }
                    else
                    {
                        t_lose.TranformerLose = "";
                    }

                    if (null != dt.Rows[i].ItemArray[5])
                    {
                        t_lose.LessOrMoreFlag = dt.Rows[i].ItemArray[5].ToString().Trim();
                    }
                    else
                    {
                        t_lose.LessOrMoreFlag = "";
                    }


                    list.Add(t_lose);
                    count = count - 1;
                    i = i + 1;
                }
                

                return Constant.OK;
            }
            catch (Exception)
            {
                return Constant.ERROR;
            }
        }


        /**
         * 校验变损号是否存在
         * @param       string t_lose_no：变损号
         * @param      ref bool state：false为存在，true为不存在
         * @return      int 值为Constant.OK：执行成功，值为Constant.ERROR为执行失败
         * @author      Rick
         **/
        public int checkTLoseNo(string t_lose_no, ref bool state)
        {
            string strCheck = "select count(*) from transformerloseinfo where transformerloseNo = " + t_lose_no;
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

        public int deleteTLoseById(string t_lose_no)
        {
            string deleteSql = "delete from TransformerLoseInfo where TransformerLoseNo = " + t_lose_no;
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
