using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cts.web.core.Librs
{
    /// <summary>
    /// 投资利息计算
    /// </summary>
    public class LoanCalculatorHelper
    {
        /// <summary>
        /// 等额本息法
        /// </summary>
        /// <param name="amount">投资金额</param>
        /// <param name="yearRate">年利率</param>
        /// <param name="months">投资期限，单位：月</param>
        /// <returns></returns>
        public static Interest DengerBenxi(double amount, double yearRate, int months)
        {
            Interest interest = new Interest();
            //年利率转为月利率
            var monthRate = (yearRate) / 1200.0; 
            var i = 0;
            var a = 0.0; // 偿还本息
            var b = 0.0; // 偿还利息
            var c = 0.0; // 偿还本金
            //利息收益
            var totalRateIncome =
                (amount * months * monthRate * Math.Pow((1 + monthRate), months)) / (Math.Pow((1 + monthRate), months) - 1) - amount;
            var totalIncome = totalRateIncome + amount;
            var d = amount + totalRateIncome; // 剩余本金

            interest.TotalMoney = (decimal)Math.Round(totalRateIncome * 100) / 100;// 支付总利息
            interest.TotalRate = (decimal)Math.Round(totalIncome * 100) / 100;
            a = totalIncome / months;    //每月还款本息
            a = Math.Round(a * 100) / 100;//每月还款本息

            for (i = 1; i <= months; i++)
            {
                b = (amount * monthRate * (Math.Pow((1 + monthRate), months) - Math.Pow((1 + monthRate), (i - 1)))) / (Math.Pow((1 + monthRate), months) - 1);
                b = Math.Round(b * 100) / 100;
                c = a - b;
                c = Math.Round(c * 100) / 100;
                d = d - a;
                d = Math.Round(d * 100) / 100;
                if (i == months)
                {
                    c = c + d;
                    b = b - d;
                    c = Math.Round(c * 100) / 100;
                    b = Math.Round(b * 100) / 100;
                    d = 0;
                }

                var unit = new BackUnit();
                unit.Number = i;// 期数 
                unit.ShouldAmount = (decimal)a;// 偿还本息  someNumber.ToString("N2");
                unit.ShouldInterest = (decimal)b;// 偿还利息
                unit.Corpus = (decimal)c;// 偿还本金
                unit.SurplusCorpus = (decimal)d;// 剩余本金
                interest.DataList.Add(unit);
            }
            return interest;
        }

        /// <summary>
        /// 先息后本。按月付息到期还本
        /// 例子。如，小明在银行贷款10万，期限为一年，年利息为5.6%，按照先息后本的还款方式，每月还款金额为100000*5.6%/12=466.67元，一年到期后再还清10万元本金。
        /// </summary>
        /// <param name="amount">投资金额</param>
        /// <param name="yearRate">年利率</param>
        /// <param name="months">投资期限，单位：月</param>
        /// <returns></returns>
        public static Interest XianxiHouben(decimal amount, decimal yearRate, int months)
        {
            Interest interest = new Interest();
            //总利息
            decimal rateIncome = Math.Round(amount * yearRate / 100.00m * (months / 12.00m), 2);
            //每月利息
            decimal rateIncomeEve = Math.Round((rateIncome / months), 2);
            //总还款额
            var total = Math.Round(amount + rateIncome, 2);

            for (var i = 1; i < months; i++)
            {
                interest.DataList.Add(new BackUnit()
                {
                    Number = i,
                    ShouldAmount = rateIncomeEve,
                    ShouldInterest = rateIncomeEve,
                    Corpus = 0,
                    SurplusCorpus = amount,
                });
            }
            //最后一期还本和利息
            interest.DataList.Add(new BackUnit()
            {
                Number = months,
                ShouldAmount = Math.Round(amount + rateIncomeEve, 2),
                ShouldInterest = rateIncomeEve,
                Corpus = amount,
                SurplusCorpus = 0
            });
            return interest;
        }

        /// <summary>
        /// 一次性还本付息
        /// </summary>
        /// <param name="amount">投资金额</param>
        /// <param name="yearRate">年利率</param>
        /// <param name="months">投资期限，单位：月</param>
        /// <returns></returns>
        public static Interest YiciXing(decimal amount, decimal yearRate, int months)
        {
            Interest interest = new Interest();
            var rate = yearRate;
            // 总利息
            var rateIncome = Math.Round(amount * rate / 100.00m * (months / 12.00m), 2);
            // 总还款
            var totalIncome = Math.Round(amount + rateIncome, 2);
            interest.TotalRate = rateIncome;
            interest.TotalMoney = totalIncome;
            interest.DataList.Add(new BackUnit()
            {
                Number = 1,
                ShouldAmount = totalIncome,
                ShouldInterest = rateIncome,
                Corpus = amount,
                SurplusCorpus = 0
            });
            return interest;
        }

        /// <summary>
        /// 等额本金
        /// 每月还款金额= （贷款本金/ 还款月数）+（本金 — 已归还本金累计额）×每月利率
        /// </summary>
        /// <param name="amount">投资金额</param>
        /// <param name="yearRate">年利率</param>
        /// <param name="months">投资期限，单位：月</param>
        /// <returns></returns>
        public static Interest DengerBenjin(decimal amount, decimal yearRate, int months)
        {
            Interest interest = new Interest();
            var monthRate = yearRate / 1200.00m;//月利率
            var monthIncome = amount / months;//月本金 
            var amountIncome = amount;
            for (int i = 1; i <= months; i++)
            {
                //月利息
                var rateIncome = Math.Round(amountIncome * monthRate, 2);
                //总利息累加
                interest.TotalRate = Math.Round(interest.TotalRate + rateIncome, 2);
                //剩余本金
                amountIncome = Math.Round(amountIncome - monthIncome, 2);
                interest.DataList.Add(new BackUnit()
                {
                    Number = i,
                    ShouldAmount = Math.Round(monthIncome + rateIncome, 2),
                    ShouldInterest = rateIncome,
                    Corpus = Math.Round(monthIncome, 2),
                    SurplusCorpus = Math.Round(amountIncome, 2)
                });
            }
            //总还款
            interest.TotalMoney = Math.Round(amount + interest.TotalRate, 2);
            return interest;
        }

    }

    /// <summary>
    /// 每期详情
    /// </summary>
    public class Interest
    {
        /// <summary>
        /// 
        /// </summary>
        public Interest()
        {
            DataList = new List<BackUnit>();
        }

        /// <summary>
        /// 总利息
        /// </summary>
        public decimal TotalRate { get; set; }

        /// <summary>
        /// 总还款
        /// </summary>
        public decimal TotalMoney { get; set; }

        /// <summary>
        /// 利息每期详细
        /// </summary>
        public List<BackUnit> DataList { get; set; }
    }

    /// <summary>
    /// 每月利息单元
    /// </summary>
    public class BackUnit
    {
        /// <summary>
        /// 当前期数
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 本期应还总额
        /// </summary>
        public decimal ShouldAmount { get; set; }

        /// <summary>
        /// 本期应还利息
        /// </summary>
        public decimal ShouldInterest { get; set; }

        /// <summary>
        /// 本期应还本金
        /// </summary>
        public decimal Corpus { get; set; }

        /// <summary>
        /// 本期剩余本金
        /// </summary>
        public decimal SurplusCorpus { get; set; }


    }






}
