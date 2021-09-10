using UnityEngine;

namespace Stoicode.UniLib.Math
{
    public class Fraction
    {
        // Fraction Numerator
        public int Numerator { get; private set; }

        // Fraction Denominator
        public int Denominator { get; private set; }

        // Chance value
        public double Chance { get; private set; }


        /// <summary>
        /// Class constructor using fraction
        /// </summary>
        /// <param name="numerator"></param>
        /// <param name="denominator"></param>
        public Fraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;

            CalculateChance();
        }

        /// <summary>
        /// Class Constructor using chance
        /// </summary>
        /// <param name="chance">Percentage chance</param>
        public Fraction(double chance)
        {
            Chance = chance;

            CalculateFraction();
        }

        /// <summary>
        /// Get fraction text
        /// </summary>
        /// <returns>Fraction string</returns>
        public string FractionText()
        {
            return $"{Numerator}/{Denominator}";
        }

        /// <summary>
        /// Get percentage text
        /// </summary>
        /// <returns>Percentage chance string</returns>
        public string PercentageText()
        {
            return $"{Chance / 100.0:0.####%}";
        }

        /// <summary>
        /// Get attempt count based on probability
        /// </summary>
        /// <param name="probability">Percentage chance</param>
        /// <returns>Calculated attempt count</returns>
        public int GetCountByProbability(double probability)
        {
            var baseFailChance = ChanceToFail();

            var attempts = 0;
            for (var i = 0; i < int.MaxValue; i++)
            {
                attempts++;

                var failChance = Mathf.Pow((float) baseFailChance, i);
                var chance = (1.0 - failChance) * 100.0;
                if (chance >= probability)
                    break;
            }

            return attempts;
        }

        /// <summary>
        /// Get percentage chance to succeed for x attempts
        /// </summary>
        /// <param name="count">Attempt count</param>
        /// <returns>Percentage chance to succeed once</returns>
        public double GetProbabilityByCount(int count)
        {
            var chanceToFail = Mathf.Pow((float) ChanceToFail(), count);

            return (1.0 - chanceToFail) * 100.0;
        }

        /// <summary>
        /// Calculate chance from fraction
        /// </summary>
        private void CalculateChance()
        {
            Chance = (double) Numerator / Denominator * 100.0;
        }

        /// <summary>
        /// Calculate fraction using percentage chance
        /// </summary>
        private void CalculateFraction()
        {
            var chance = Chance / 100.0;
            const double accuracy = 0.001;

            var sign = chance < 0 ? -1 : 1;
            chance = chance < 0 ? -chance : chance;

            var inted = (int) chance;
            chance -= inted;

            var min = chance - accuracy;
            if (min < 0.0)
            {
                Denominator = sign * inted;
                Numerator = 1;

                return;
            }

            var max = chance + accuracy;
            if (max > 1.0)
            {
                Denominator = sign * (inted + 1);
                Numerator = 1;

                return;
            }

            var a = 0;
            var b = 1;
            var c = 1;
            var d = (int) (1 / max);

            while (true)
            {
                var n = (int) ((b * min - a) / (c - d * min));
                if (n == 0) break;

                a += n * c;
                b += n * d;
                n = (int) ((c - d * max) / (b * max - a));

                if (n == 0) break;

                c += n * a;
                d += n * b;
            }

            Denominator = b + d;
            Numerator = sign * (inted * Denominator + (a + c));
        }

        /// <summary>
        /// Get chance to fail (real percentage value so 25% = 0.25)
        /// </summary>
        /// <returns></returns>
        private double ChanceToFail()
        {
            return (100.0 - Chance) / 100.0;
        }
    }
}