using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace testBlackJack
{
    [TestClass]
    public class TestSumCards
    {
        sharpBlackJack.Game blackJack = new sharpBlackJack.Game();
        sharpBlackJack.Croupier deealer = new sharpBlackJack.Croupier();

        [TestMethod]
        public void Test_SumCards()
        {
            blackJack.StartDeal();

            Assert.AreNotEqual(blackJack.Stakeholders[0].MyHand.SumCards(), 0);
        }
    }
}
