using WeightliftingManagment.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using Xunit;
using WeightliftingManagment.MvvmSupport.Collections;

namespace WeightliftingManagment.Domain.Model.Tests
{
    public class ParticipantTests
    {
        public Participant Model;

        public ParticipantTests()
        {
            Model = Participant.CreateBuilder()
                .WithFirstAndLastName("Paweł", "Gawęda")
                .WithClub("KPC Hejnał Kęty")
                .WithYearOfBirth(2020)
                .WithBodyWeight(113)
                .WithSnatchs(new(new(100, AttemptStatus.GoodLift), new(110, AttemptStatus.GoodLift), new(120, AttemptStatus.Declared)))
                .WithCleanJerks(new(new(100, AttemptStatus.GoodLift), new(110, AttemptStatus.GoodLift), new(120, AttemptStatus.Declared)))
                .Build();
        }

        [Theory]
        [MemberData(nameof(DataToMaxTest))]
        public void Max_Rwanie_test(int val1, AttemptStatus result1, int val2, AttemptStatus result2, int val3, AttemptStatus result3, int exp, int exp2)
        {
            var model = Participant.CreateBuilder()
                .WithSnatchs(new(new(val1, result1), new(val2, result2), new(val3, result3)))
                .Build();

            model.DesignateMaxOfSnatch();

            model.MaxOfSnatch.Should().Be(exp);
            model.NumberMaxOfSnatch.Should().Be(exp2);
            model.Snatchs.Collection.MaxBy(item => item.AttemptToInt())?.StatusIsMax().Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(DataToMaxTest))]
        public void Max_Podrzut_test(int val1, AttemptStatus result1, int val2, AttemptStatus result2, int val3, AttemptStatus result3, int exp, int exp2)
        {
            var model = Participant.CreateBuilder()
                .WithCleanJerks(new(new(val1, result1), new(val2, result2), new(val3, result3)))
                .Build();

            model.DesignateMaxOfCleanJerk();

            model.MaxOfCleanJerk.Should().Be(exp);
            model.NumberMaxOfCleanJerk.Should().Be(exp2);
            model.CleanJerks.Collection.MaxBy(item => item.AttemptToInt())?.StatusIsMax().Should().BeTrue();
        }

        [Theory]
        [InlineData(AttemptStatus.GoodLift, AttemptStatus.GoodLift)]
        [InlineData(AttemptStatus.NoLift, AttemptStatus.NoLift)]
        [InlineData(AttemptStatus.Resignation, AttemptStatus.Resignation)]
        public void SetAttempt_Snatch_Test(AttemptStatus result, AttemptStatus expected)
        {
            Model.Snatchs[2].SetStatus(AttemptStatus.Declared);

            Model.SetAttempt(result, true);

            Model.Snatchs[2].Status.Should().Be(expected);
        }

        [Theory]
        [InlineData(AttemptStatus.GoodLift, AttemptStatus.GoodLift)]
        [InlineData(AttemptStatus.NoLift, AttemptStatus.NoLift)]
        [InlineData(AttemptStatus.Resignation, AttemptStatus.Resignation)]
        public void SetAttempt_CleanJerk_Test(AttemptStatus result, AttemptStatus expected)
        {
            Model.CleanJerks[2].SetStatus(AttemptStatus.Declared);

            Model.SetAttempt(result, false);

            Model.CleanJerks[2].Status.Should().Be(expected);
        }

        [Fact]
        public void ClearAttemptResignation_test()
        {
            var model = Participant.CreateBuilder()
                .WithSnatchs(new(new(100, AttemptStatus.Resignation), new(110, AttemptStatus.Resignation), new(120, AttemptStatus.Resignation)))
                .Build();

            model.ClearAttemptResignation(0, true);

            model.Snatchs[0].StatusIsDeclared().Should().BeTrue();
            model.Snatchs[1].StatusIsNoDeclared().Should().BeTrue();
            model.Snatchs[2].StatusIsNoDeclared().Should().BeTrue();
        }

        [Fact]
        public void ClearAttemptResignation_throw_ArgumentException_test()
        {
            var model = new Participant();
            Assert.Throws<ArgumentException>(() => model.ClearAttemptResignation(4, false));
        }

        [Fact]
        public void SetAttemptValueFromLastAttemptValue_throw_exception_test()
        {
            var model = new Participant {
                Snatchs = new AttemptCollection(120, AttemptStatus.Declared)
            };
            var result = Assert.Throws<ArgumentException>(() => model.SetAttemptValueFromLastAttemptValue(5, true));

        }
        [Fact]
        public void SetAttemptValueFromLastAttemptValue_default_throw_exception_test()
        {
            var model = new Participant();
            var result = Assert.Throws<ArgumentException>(() => model.SetAttemptValueFromLastAttemptValue(5, false));

        }
        [Fact]
        public void SetAttemptValueFromLastAttemptValue_test()
        {
            var model = Participant.CreateBuilder()
                .WithSnatchs(new(new(120, AttemptStatus.GoodLift), new(5, AttemptStatus.Declared), new()))
                .Build();

            model.SetAttemptValueFromLastAttemptValue(1, true);
            model.Snatchs[1].Value.Should().Be(125);

        }

        [Fact]
        public void SetAttempt_Podrzut_1_2_zaliczone_test()
        {
            var model = new Participant {
                CleanJerks = new()
            };
            model.CleanJerks[0].SetValue(100);
            model.CleanJerks[0].SetStatus(AttemptStatus.ComesUp);
            model.SetAttempt(0, AttemptStatus.GoodLift, false);

            model.CleanJerks[0].StatusIsGoodLift().Should().BeTrue();
            model.CleanJerks[1].Value.Should().Be(101);
            model.CleanJerks[1].StatusIsDeclared().Should().BeTrue();

        }

        [Fact]
        public void SetAttempt_Podrzut_2_3_zaliczone_test()
        {
            var model = new Participant {
                CleanJerks = new()
            };
            model.CleanJerks[1].SetValue(100);
            model.CleanJerks[1].SetStatus(AttemptStatus.ComesUp);
            model.SetAttempt(1, AttemptStatus.GoodLift, false);

            model.CleanJerks[1].StatusIsGoodLift().Should().BeTrue();
            model.CleanJerks[2].Value.Should().Be(101);
            model.CleanJerks[2].StatusIsDeclared().Should().BeTrue();
        }

        [Fact]
        public void SetAttempt_Podrzut_1_2_spalone_test()
        {
            var model = new Participant {
                CleanJerks = new()
            };
            model.CleanJerks[0].SetValue(100);
            model.CleanJerks[0].SetStatus(AttemptStatus.ComesUp);
            model.SetAttempt(0, AttemptStatus.NoLift, false);

            model.CleanJerks[0].StatusIsNoLift().Should().BeTrue();
            model.CleanJerks[1].Value.Should().Be(100);
            model.CleanJerks[1].StatusIsDeclared().Should().BeTrue();
        }

        [Fact]
        public void SetAttempt_Podrzut_2_3_spalone_test()
        {
            var model = new Participant {
                CleanJerks = new()
            };
            model.CleanJerks[1].SetValue(100);
            model.CleanJerks[1].SetStatus(AttemptStatus.ComesUp);
            model.SetAttempt(1, AttemptStatus.NoLift, false);

            model.CleanJerks[1].StatusIsNoLift().Should().BeTrue();
            model.CleanJerks[2].Value.Should().Be(100);
            model.CleanJerks[2].StatusIsDeclared().Should().BeTrue();
        }

        [Fact]
        public void SetAttempt_Rwanie_1_2_zaliczone_test()
        {
            var model = new Participant {
                Snatchs = new()
            };
            model.Snatchs[0].SetValue(100);
            model.Snatchs[0].SetStatus(AttemptStatus.ComesUp);
            model.SetAttempt(0, AttemptStatus.GoodLift, true);



            model.Snatchs.First.StatusIsGoodLift().Should().BeTrue();
            model.Snatchs.Second.Value.Should().Be(101);
            model.Snatchs.Second.StatusIsDeclared().Should().BeTrue();
        }

        [Fact]
        public void SetAttempt_Rwanie_2_3_zaliczone_test()
        {
            var model = new Participant {
                Snatchs = new()
            };
            model.Snatchs[1].SetValue(100);
            model.Snatchs[1].SetStatus(AttemptStatus.ComesUp);
            model.SetAttempt(1, AttemptStatus.GoodLift, true);

            model.Snatchs[1].StatusIsGoodLift().Should().BeTrue();
            model.Snatchs[2].Value.Should().Be(101);
            model.Snatchs[2].StatusIsDeclared().Should().BeTrue();
        }

        [Fact()]
        public void SetAttempt_Rwanie_1_2_spalone_test()
        {
            var model = new Participant {
                Snatchs = new()
            };
            model.Snatchs[0].SetValue(100);
            model.Snatchs[0].SetStatus(AttemptStatus.ComesUp);
            model.SetAttempt(0, AttemptStatus.NoLift, true);

            model.Snatchs[0].StatusIsNoLift().Should().BeTrue();
            model.Snatchs[1].Value.Should().Be(100);
            model.Snatchs[1].StatusIsDeclared().Should().BeTrue();
        }

        [Fact]
        public void SetAttempt_Rwanie_2_3_spalone_test()
        {
            var model = new Participant {
                Snatchs = new()
            };
            model.Snatchs[1].SetValue(100);
            model.Snatchs[1].SetStatus(AttemptStatus.ComesUp);
            model.SetAttempt(1, AttemptStatus.NoLift, true);

            model.Snatchs[1].StatusIsNoLift().Should().BeTrue();
            model.Snatchs[2].Value.Should().Be(100);
            model.Snatchs[2].StatusIsDeclared().Should().BeTrue();
        }

        [Fact]
        public void SetAttempt_RwanieNr3_test()
        {
            var model = new Participant {
                Snatchs = new(
                new Attempt(100, AttemptStatus.GoodLift),
                new Attempt(110, AttemptStatus.GoodLift),
                new Attempt(120, AttemptStatus.ComesUp)
                ),
                CleanJerks = new(
                new Attempt(200, AttemptStatus.GoodLift),
                new Attempt(210, AttemptStatus.GoodLift),
                new Attempt(220, AttemptStatus.ComesUp)
                )
            };
            model.SetAttempt(AttemptStatus.GoodLift, true);
            model.Snatchs[2].StatusIsGoodLift().Should().BeTrue();
        }

        [Fact]
        public void SetAttempt_PodrzutNr3_test()
        {
            var model = new Participant {
                Snatchs = new(
                new Attempt(100, AttemptStatus.GoodLift),
                new Attempt(110, AttemptStatus.GoodLift),
                new Attempt(120, AttemptStatus.ComesUp)
                ),
                CleanJerks = new(
                new Attempt(200, AttemptStatus.GoodLift),
                new Attempt(210, AttemptStatus.GoodLift),
                new Attempt(220, AttemptStatus.ComesUp)
      )
            };
            model.SetAttempt(AttemptStatus.GoodLift, false);
            model.CleanJerks[2].StatusIsGoodLift().Should().BeTrue();
        }

        [Theory]
        [InlineData(1, true, 3)]
        [InlineData(2, true, 2)]
        [InlineData(3, true, 1)]
        public void SetRezygnacjaRwanieTest(int numerPodejscia, bool flagRwanie, int expected)
        {
            var model = new Participant {
                Snatchs = new(
                new Attempt(100, AttemptStatus.Next),
                new Attempt(110, AttemptStatus.ComesUp),
                new Attempt(120, AttemptStatus.Next)
                ),
                CleanJerks = new(
                new Attempt(200, AttemptStatus.ComesUp),
                new Attempt(210, AttemptStatus.Next),
                new Attempt(220, AttemptStatus.ComesUp)
                )
            };
            model.SetAttemptResignation(numerPodejscia, flagRwanie);
            model.Snatchs.Collection.Count(i => i.StatusIsResignation()).Should().Be(expected);
        }

        [Theory]
        [InlineData(1, false, 3)]
        [InlineData(2, false, 2)]
        [InlineData(3, false, 1)]
        public void SetRezygnacjaPodrzutTest(int numerPodejscia, bool flagRwanie, int expected)
        {
            var model = new Participant {
                Snatchs = new(
                new Attempt(100, AttemptStatus.Next),
                new Attempt(110, AttemptStatus.ComesUp),
                new Attempt(120, AttemptStatus.Next)
                ),
                CleanJerks = new(
                new Attempt(200, AttemptStatus.ComesUp),
                new Attempt(210, AttemptStatus.Next),
                new Attempt(220, AttemptStatus.ComesUp)
                )
            };
            model.SetAttemptResignation(numerPodejscia, flagRwanie);
            model.CleanJerks.Collection.Count(i => i.StatusIsResignation()).Should().Be(expected);
        }



        [Fact]
        public void CzyszczenieNastepny_podchodzi_test()
        {
            var model = new Participant {
                Snatchs = new(
                new Attempt(100, AttemptStatus.Next),
                new Attempt(110, AttemptStatus.ComesUp),
                new Attempt(120, AttemptStatus.Next)
                ),
                CleanJerks = new(
                new Attempt(200, AttemptStatus.ComesUp),
                new Attempt(210, AttemptStatus.Next),
                new Attempt(220, AttemptStatus.ComesUp)
                )
            };
            model.ClearNextAndComesUp();

            model.Snatchs.Collection.Count(i => i.StatusIsDeclared()).Should().Be(3);
            model.CleanJerks.Collection.Count(i => i.StatusIsDeclared()).Should().Be(3);
        }

        [Theory]
        [InlineData(true, 1, 100)]
        [InlineData(true, 2, 110)]
        [InlineData(true, 3, 120)]
        [InlineData(false, 1, 200)]
        [InlineData(false, 2, 210)]
        [InlineData(false, 3, 220)]
        public void GetAttemptWeightOfNumber_test(bool isrwanie, int number, int expected)
        {
            var model = new Participant {
                Snatchs = new(new(100), new(110), new(120)),
                CleanJerks = new(new(200), new(210), new(220))
            };
            model.GetAttemptWeightOfNumber(number, isrwanie).Should().Be(expected);

        }

        [Theory]
        [InlineData(true, AttemptStatus.Declared, AttemptStatus.NoDeclared, AttemptStatus.NoDeclared, 1)]
        [InlineData(true, AttemptStatus.NoLift, AttemptStatus.Declared, AttemptStatus.NoDeclared, 2)]
        [InlineData(true, AttemptStatus.GoodLift, AttemptStatus.Declared, AttemptStatus.NoDeclared, 2)]
        [InlineData(true, AttemptStatus.GoodLift, AttemptStatus.GoodLift, AttemptStatus.Declared, 3)]
        [InlineData(true, AttemptStatus.GoodLift, AttemptStatus.NoLift, AttemptStatus.Declared, 3)]
        [InlineData(true, AttemptStatus.NoLift, AttemptStatus.NoLift, AttemptStatus.Declared, 3)]
        [InlineData(false, AttemptStatus.Declared, AttemptStatus.NoDeclared, AttemptStatus.NoDeclared, 1)]
        [InlineData(false, AttemptStatus.NoLift, AttemptStatus.Declared, AttemptStatus.NoDeclared, 2)]
        [InlineData(false, AttemptStatus.GoodLift, AttemptStatus.Declared, AttemptStatus.NoDeclared, 2)]
        [InlineData(false, AttemptStatus.GoodLift, AttemptStatus.GoodLift, AttemptStatus.Declared, 3)]
        [InlineData(false, AttemptStatus.GoodLift, AttemptStatus.NoLift, AttemptStatus.Declared, 3)]
        [InlineData(false, AttemptStatus.NoLift, AttemptStatus.NoLift, AttemptStatus.Declared, 3)]
        public void GetNumerAnnonsu_test(bool isrwanie, AttemptStatus result1, AttemptStatus result2, AttemptStatus result3, int expected)
        {
            var model = new Participant {
                Snatchs = new(new(100, result1), new(110, result2), new(120, result3)),
                CleanJerks = new(new(200, result1), new(210, result2), new(220, result3))
            };

            model.GetNumberOfDeclared(isrwanie).Should().Be(expected);
        }

        [Theory]
        [InlineData(true, AttemptStatus.Declared, AttemptStatus.NoDeclared, AttemptStatus.NoDeclared, 100)]
        [InlineData(true, AttemptStatus.NoLift, AttemptStatus.Declared, AttemptStatus.NoDeclared, 110)]
        [InlineData(true, AttemptStatus.GoodLift, AttemptStatus.Declared, AttemptStatus.NoDeclared, 110)]
        [InlineData(true, AttemptStatus.GoodLift, AttemptStatus.GoodLift, AttemptStatus.Declared, 120)]
        [InlineData(true, AttemptStatus.GoodLift, AttemptStatus.NoLift, AttemptStatus.Declared, 120)]
        [InlineData(true, AttemptStatus.NoLift, AttemptStatus.NoLift, AttemptStatus.Declared, 120)]
        [InlineData(false, AttemptStatus.Declared, AttemptStatus.NoDeclared, AttemptStatus.NoDeclared, 200)]
        [InlineData(false, AttemptStatus.NoLift, AttemptStatus.Declared, AttemptStatus.NoDeclared, 210)]
        [InlineData(false, AttemptStatus.GoodLift, AttemptStatus.Declared, AttemptStatus.NoDeclared, 210)]
        [InlineData(false, AttemptStatus.GoodLift, AttemptStatus.GoodLift, AttemptStatus.Declared, 220)]
        [InlineData(false, AttemptStatus.GoodLift, AttemptStatus.NoLift, AttemptStatus.Declared, 220)]
        [InlineData(false, AttemptStatus.NoLift, AttemptStatus.NoLift, AttemptStatus.Declared, 220)]
        public void GetAnnonsWeight_test(bool isrwanie, AttemptStatus result1, AttemptStatus result2, AttemptStatus result3, int expected)
        {
            var model = new Participant {
                Snatchs = new(new(100, result1), new(110, result2), new(120, result3)),
                CleanJerks = new(new(200, result1), new(210, result2), new(220, result3))
            };

            model.GetDeclaredWeight(isrwanie).Should().Be(expected);
        }

        [Theory]
        [InlineData(true, AttemptStatus.Declared, AttemptStatus.NoDeclared, AttemptStatus.NoDeclared, 100)]
        [InlineData(true, AttemptStatus.NoLift, AttemptStatus.Declared, AttemptStatus.NoDeclared, 110)]
        [InlineData(true, AttemptStatus.GoodLift, AttemptStatus.Declared, AttemptStatus.NoDeclared, 110)]
        [InlineData(true, AttemptStatus.GoodLift, AttemptStatus.GoodLift, AttemptStatus.Declared, 120)]
        [InlineData(true, AttemptStatus.GoodLift, AttemptStatus.NoLift, AttemptStatus.Declared, 120)]
        [InlineData(true, AttemptStatus.NoLift, AttemptStatus.NoLift, AttemptStatus.Declared, 120)]
        [InlineData(false, AttemptStatus.Declared, AttemptStatus.NoDeclared, AttemptStatus.NoDeclared, 200)]
        [InlineData(false, AttemptStatus.NoLift, AttemptStatus.Declared, AttemptStatus.NoDeclared, 210)]
        [InlineData(false, AttemptStatus.GoodLift, AttemptStatus.Declared, AttemptStatus.NoDeclared, 210)]
        [InlineData(false, AttemptStatus.GoodLift, AttemptStatus.GoodLift, AttemptStatus.Declared, 220)]
        [InlineData(false, AttemptStatus.GoodLift, AttemptStatus.NoLift, AttemptStatus.Declared, 220)]
        [InlineData(false, AttemptStatus.NoLift, AttemptStatus.NoLift, AttemptStatus.Declared, 220)]
        public void GetAnnons_Test_test(bool isrwanie, AttemptStatus result1, AttemptStatus result2, AttemptStatus result3, int expected)
        {
            var model = new Participant {
                Snatchs = new(new(100, result1), new(110, result2), new(120, result3)),
                CleanJerks = new(new(200, result1), new(210, result2), new(220, result3))
            };
            model.GetDeclared(isrwanie)?.Value.Should().Be(expected);
            model.GetDeclared(isrwanie)?.Status.Should().Be(AttemptStatus.Declared);

        }

        [Theory]
        [InlineData(100, 120, 220)]
        [InlineData(120, 120, 240)]
        [InlineData(65, 85, 150)]
        public void ObliczDwuboj_Test(int maxr, int maxp, int exp)
        {
            var model = new Participant {
                MaxOfSnatch = maxr,
                MaxOfCleanJerk = maxp
            };
            model.CountTotal();
            model.Total.Should().Be(exp);

        }

        [Theory]
        [InlineData(100, 2.0, 20, 220)]
        [InlineData(120, 1.5, 0, 180)]
        [InlineData(200, 1.2, 35, 275)]
        public void ObliczPunkty_test(int total, double wsp, int bonifikat, double exp)
        {
            var model = new Participant {
                Total = total,
                SinclairCoefficient = wsp,
                BonusPoint = bonifikat
            };
            model.CountPoints();
            model.Points.Should().Be(exp);

        }

        [Theory]
        [InlineData(true, AttemptStatus.ComesUp, AttemptStatus.NoDeclared, AttemptStatus.NoDeclared, 1)]
        [InlineData(true, AttemptStatus.NoLift, AttemptStatus.ComesUp, AttemptStatus.NoDeclared, 2)]
        [InlineData(true, AttemptStatus.GoodLift, AttemptStatus.ComesUp, AttemptStatus.NoDeclared, 2)]
        [InlineData(true, AttemptStatus.GoodLift, AttemptStatus.GoodLift, AttemptStatus.ComesUp, 3)]
        [InlineData(true, AttemptStatus.GoodLift, AttemptStatus.NoLift, AttemptStatus.ComesUp, 3)]
        [InlineData(true, AttemptStatus.NoLift, AttemptStatus.NoLift, AttemptStatus.ComesUp, 3)]
        [InlineData(false, AttemptStatus.ComesUp, AttemptStatus.NoDeclared, AttemptStatus.NoDeclared, 1)]
        [InlineData(false, AttemptStatus.NoLift, AttemptStatus.ComesUp, AttemptStatus.NoDeclared, 2)]
        [InlineData(false, AttemptStatus.GoodLift, AttemptStatus.ComesUp, AttemptStatus.NoDeclared, 2)]
        [InlineData(false, AttemptStatus.GoodLift, AttemptStatus.GoodLift, AttemptStatus.ComesUp, 3)]
        [InlineData(false, AttemptStatus.GoodLift, AttemptStatus.NoLift, AttemptStatus.ComesUp, 3)]
        [InlineData(false, AttemptStatus.NoLift, AttemptStatus.NoLift, AttemptStatus.ComesUp, 3)]
        public void GetNumerPodchodzi_test(bool isrwanie, AttemptStatus result1, AttemptStatus result2, AttemptStatus result3, int expected)
        {
            var model = new Participant {
                Snatchs = new(new(100, result1), new(110, result2), new(120, result3)),
                CleanJerks = new(new(200, result1), new(210, result2), new(220, result3))
            };

            model.GetNumberOfComesUp(isrwanie).Should().Be(expected);

        }

        [Fact]
        public void IsRwanieAnnons_test()
        {
            var model = new Participant {
                Snatchs = new(100),
                CleanJerks = new(200)
            };

            model.IsSnatchDeclared().Should().BeTrue();
        }

        [Fact]
        public void IsNotRwanieAnnons_test()
        {
            var model = new Participant {
                Snatchs = new(new(100, AttemptStatus.GoodLift), new(110, AttemptStatus.GoodLift), new(120, AttemptStatus.GoodLift)),
                CleanJerks = new(new(200, AttemptStatus.ComesUp), new(), new())
            };
            model.IsSnatchDeclared().Should().BeFalse();
        }

        [Theory]
        [InlineData(86, 1986)]
        [InlineData(11, 2011)]
        [InlineData(1, 2001)]
        [InlineData(0, 2000)]
        public void Napraw_Rok_test(int rok, int expected)
        {
            var model = new Participant {
                YearOfBirth = rok
            };
            model.RepairYear();
            model.YearOfBirth.Should().Be(expected);
        }

        [Theory]
        [InlineData(100, 120, 220)]
        [InlineData(130, 150, 280)]
        [InlineData(150, 170, 320)]
        [InlineData(140, 160, 300)]
        [InlineData(120, 140, 260)]
        public void CountPromiseTotal_Test(int snatch, int cleanJerk, int promiseTotal)
        {
            var model = new Participant {
                Snatchs = new(snatch),
                CleanJerks = new(cleanJerk)
            };
            model.CountPromiseTotal();
            model.PromiseTotal.Should().Be(promiseTotal);
        }

        [Fact()]
        public void CountPromisePoint_Test()
        {
            var model = new Participant {
                PromiseTotal = 100,
                SinclairCoefficient = 1.5,
                BonusPoint = 30
            };
            model.CountPromisePoint();
            model.PromisePoint.Should().Be(180);
        }

        [Theory]
        [MemberData(nameof(DataToComesUpValue))]
        public void GetComesUpValue_Test(AttemptCollection snatches, AttemptCollection cleanJerks, int value)
        {
            var model = new Participant {
                Snatchs = snatches,
                CleanJerks = cleanJerks
            };

            model.GetComesUpValue().Should().Be(value);
        }


        [Theory]
        [MemberData(nameof(DataToSetComesUp))]
        public void SetComesUpTest(bool isRwanie, AttemptCollection snatches, AttemptCollection cleanJerks)
        {
            var model = new Participant {
                Snatchs = snatches,
                CleanJerks = cleanJerks
            };

            model.SetComesUp(isRwanie);
            model.ComesUp.Should().BeTrue();
        }

        [Theory()]
        [MemberData(nameof(DataToGetNextValue))]
        public void GetNextValueTest(AttemptCollection snatches, AttemptCollection cleanJerks, int value)
        {
            var model = new Participant {
                Snatchs = snatches,
                CleanJerks = cleanJerks
            };
            model.GetNextValue().Should().Be(value);
        }

        [Theory]
        [MemberData(nameof(DataToSetNext))]
        public void SetNextTest(bool isRwanie, AttemptCollection snatches, AttemptCollection cleanJerks)
        {
            var model = new Participant {
                Snatchs = snatches,
                CleanJerks = cleanJerks
            };

            model.SetNext(isRwanie);
            model.Next.Should().BeTrue();
        }


        public static IEnumerable<object[]> DataToComesUpValue =>
        new List<object[]>
        {
            new object[]
            {
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.ComesUp),
                    new Attempt(110, AttemptStatus.NoDeclared),
                    new Attempt(120, AttemptStatus.NoDeclared)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.Declared),
                    new Attempt(210, AttemptStatus.NoDeclared),
                    new Attempt(220, AttemptStatus.NoDeclared)
                ),
                100
            },
           new object[]
           {
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.Max),
                    new Attempt(110, AttemptStatus.ComesUp),
                    new Attempt(120, AttemptStatus.NoDeclared)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.Declared),
                    new Attempt(210, AttemptStatus.NoDeclared),
                    new Attempt(220, AttemptStatus.NoDeclared)
                ),
               110
            },
           new object[]
           {
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.Max),
                    new Attempt(110, AttemptStatus.NoLift),
                    new Attempt(120, AttemptStatus.ComesUp)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.Declared),
                    new Attempt(210, AttemptStatus.NoDeclared),
                    new Attempt(220, AttemptStatus.NoDeclared)
                ),
               120
            },
           new object[]
           {
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.GoodLift),
                    new Attempt(110, AttemptStatus.NoLift),
                    new Attempt(120, AttemptStatus.Max)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.ComesUp),
                    new Attempt(210, AttemptStatus.NoDeclared),
                    new Attempt(220, AttemptStatus.NoDeclared)
                ),
               200
            },
           new object[]
           {
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.GoodLift),
                    new Attempt(110, AttemptStatus.NoLift),
                    new Attempt(120, AttemptStatus.Max)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.Max),
                    new Attempt(210, AttemptStatus.ComesUp),
                    new Attempt(220, AttemptStatus.NoDeclared)
                ),
               210
            },
           new object[]
           {
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.GoodLift),
                    new Attempt(110, AttemptStatus.NoLift),
                    new Attempt(120, AttemptStatus.Max)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.GoodLift),
                    new Attempt(210, AttemptStatus.Max),
                    new Attempt(220, AttemptStatus.ComesUp)
                ),
               220
            },
           new object[]
           {
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.GoodLift),
                    new Attempt(110, AttemptStatus.NoLift),
                    new Attempt(120, AttemptStatus.Max)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.GoodLift),
                    new Attempt(210, AttemptStatus.GoodLift),
                    new Attempt(220, AttemptStatus.NoLift)
                ),
               0
            },
        };
        public static IEnumerable<object[]> DataToSetComesUp =>
        new List<object[]>
        {
            new object[]
            {
                true,
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.ComesUp),
                    new Attempt(110, AttemptStatus.NoDeclared),
                    new Attempt(120, AttemptStatus.NoDeclared)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.Declared),
                    new Attempt(210, AttemptStatus.NoDeclared),
                    new Attempt(220, AttemptStatus.NoDeclared)
                )
            },
           new object[]
           {
               true,
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.Max),
                    new Attempt(110, AttemptStatus.ComesUp),
                    new Attempt(120, AttemptStatus.NoDeclared)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.Declared),
                    new Attempt(210, AttemptStatus.NoDeclared),
                    new Attempt(220, AttemptStatus.NoDeclared)
                )
            },
           new object[]
           {
               true,
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.Max),
                    new Attempt(110, AttemptStatus.NoLift),
                    new Attempt(120, AttemptStatus.ComesUp)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.Declared),
                    new Attempt(210, AttemptStatus.NoDeclared),
                    new Attempt(220, AttemptStatus.NoDeclared)
                )
            },
           new object[]
           {
               false,
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.GoodLift),
                    new Attempt(110, AttemptStatus.NoLift),
                    new Attempt(120, AttemptStatus.Max)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.ComesUp),
                    new Attempt(210, AttemptStatus.NoDeclared),
                    new Attempt(220, AttemptStatus.NoDeclared)
                )
            },
           new object[]
           {
               false,
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.GoodLift),
                    new Attempt(110, AttemptStatus.NoLift),
                    new Attempt(120, AttemptStatus.Max)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.Max),
                    new Attempt(210, AttemptStatus.ComesUp),
                    new Attempt(220, AttemptStatus.NoDeclared)
                )
            },
           new object[]
           {
               false,
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.GoodLift),
                    new Attempt(110, AttemptStatus.NoLift),
                    new Attempt(120, AttemptStatus.Max)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.GoodLift),
                    new Attempt(210, AttemptStatus.Max),
                    new Attempt(220, AttemptStatus.ComesUp)
                )
            }
        };
        public static IEnumerable<object[]> DataToSetNext =>
        new List<object[]>
        {
            new object[]
            {
                true,
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.Next),
                    new Attempt(110, AttemptStatus.NoDeclared),
                    new Attempt(120, AttemptStatus.NoDeclared)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.Declared),
                    new Attempt(210, AttemptStatus.NoDeclared),
                    new Attempt(220, AttemptStatus.NoDeclared)
                )
            },
           new object[]
           {
               true,
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.Max),
                    new Attempt(110, AttemptStatus.Next),
                    new Attempt(120, AttemptStatus.NoDeclared)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.Declared),
                    new Attempt(210, AttemptStatus.NoDeclared),
                    new Attempt(220, AttemptStatus.NoDeclared)
                )
            },
           new object[]
           {
               true,
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.Max),
                    new Attempt(110, AttemptStatus.NoLift),
                    new Attempt(120, AttemptStatus.Next)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.Declared),
                    new Attempt(210, AttemptStatus.NoDeclared),
                    new Attempt(220, AttemptStatus.NoDeclared)
                )
            },
           new object[]
           {
               false,
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.GoodLift),
                    new Attempt(110, AttemptStatus.NoLift),
                    new Attempt(120, AttemptStatus.Max)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.Next),
                    new Attempt(210, AttemptStatus.NoDeclared),
                    new Attempt(220, AttemptStatus.NoDeclared)
                )
            },
           new object[]
           {
               false,
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.GoodLift),
                    new Attempt(110, AttemptStatus.NoLift),
                    new Attempt(120, AttemptStatus.Max)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.Max),
                    new Attempt(210, AttemptStatus.Next),
                    new Attempt(220, AttemptStatus.NoDeclared)
                )
            },
           new object[]
           {
               false,
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.GoodLift),
                    new Attempt(110, AttemptStatus.NoLift),
                    new Attempt(120, AttemptStatus.Max)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.GoodLift),
                    new Attempt(210, AttemptStatus.Max),
                    new Attempt(220, AttemptStatus.Next)
                )
            }
        };
        public static IEnumerable<object[]> DataToGetNextValue =>
        new List<object[]>
        {
            new object[]
            {
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.Next),
                    new Attempt(110, AttemptStatus.NoDeclared),
                    new Attempt(120, AttemptStatus.NoDeclared)
               ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.Declared),
                    new Attempt(210, AttemptStatus.NoDeclared),
                    new Attempt(220, AttemptStatus.NoDeclared)
                ),
                100
            },
           new object[]
           {
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.Max),
                    new Attempt(110, AttemptStatus.Next),
                    new Attempt(120, AttemptStatus.NoDeclared)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.Declared),
                    new Attempt(210, AttemptStatus.NoDeclared),
                    new Attempt(220, AttemptStatus.NoDeclared)
                ),
               110
            },
           new object[]
           {
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.Max),
                    new Attempt(110, AttemptStatus.NoLift),
                    new Attempt(120, AttemptStatus.Next)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.Declared),
                    new Attempt(210, AttemptStatus.NoDeclared),
                    new Attempt(220, AttemptStatus.NoDeclared)
                ),
               120
            },
           new object[]
           {
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.GoodLift),
                    new Attempt(110, AttemptStatus.NoLift),
                    new Attempt(120, AttemptStatus.Max)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.Next),
                    new Attempt(210, AttemptStatus.NoDeclared),
                    new Attempt(220, AttemptStatus.NoDeclared)
                ),
               200
            },
           new object[]
           {
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.GoodLift),
                    new Attempt(110, AttemptStatus.NoLift),
                    new Attempt(120, AttemptStatus.Max)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.Max),
                    new Attempt(210, AttemptStatus.Next),
                    new Attempt(220, AttemptStatus.NoDeclared)
                ),
               210
            },
           new object[]
           {
                new AttemptCollection
                (
                    new Attempt(100, AttemptStatus.GoodLift),
                    new Attempt(110, AttemptStatus.NoLift),
                    new Attempt(120, AttemptStatus.Max)
                ),
                new AttemptCollection
                (
                    new Attempt(200, AttemptStatus.GoodLift),
                    new Attempt(210, AttemptStatus.Max),
                    new Attempt(220, AttemptStatus.Next)
                ),
               220
            }
        };
        public static IEnumerable<object[]> DataToMaxTest =>
        new List<object[]>
        {
        new object[] {100, AttemptStatus.GoodLift, 110, AttemptStatus.NoLift, 120, AttemptStatus.NoLift, 100, 1 },
        new object[] {100, AttemptStatus.GoodLift, 110, AttemptStatus.NoLift, 120, AttemptStatus.Resignation, 100, 1 },
        new object[] {100, AttemptStatus.GoodLift, 110, AttemptStatus.Resignation, 120, AttemptStatus.Resignation, 100, 1 },
        new object[] {100, AttemptStatus.GoodLift, 110, AttemptStatus.GoodLift, 120, AttemptStatus.NoLift, 110, 2 },
        new object[] {100, AttemptStatus.GoodLift, 110, AttemptStatus.GoodLift, 120, AttemptStatus.Resignation, 110, 2 },
        new object[] {100, AttemptStatus.NoLift, 110, AttemptStatus.GoodLift, 120, AttemptStatus.NoLift, 110, 2 },
        new object[] {100, AttemptStatus.NoLift, 110, AttemptStatus.GoodLift, 120, AttemptStatus.Resignation, 110, 2 },
        new object[] {100, AttemptStatus.GoodLift, 110, AttemptStatus.GoodLift, 120, AttemptStatus.GoodLift, 120, 3 },
        new object[] {100, AttemptStatus.GoodLift, 110, AttemptStatus.NoLift, 120, AttemptStatus.GoodLift, 120, 3 },
        new object[] {100, AttemptStatus.NoLift, 110, AttemptStatus.GoodLift, 120, AttemptStatus.GoodLift, 120, 3 },
        new object[] {100, AttemptStatus.NoLift, 110, AttemptStatus.NoLift, 120, AttemptStatus.GoodLift, 120, 3 }
        };

    }
}