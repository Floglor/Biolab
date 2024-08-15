using NUnit.Framework;
using Stats;


    public class StatWithMaxTests
    {
        [Test]
        public void Initialization_SetsCorrectValues()
        {
            // Arrange
            float baseValue = 50f;
            float maxValue = 100f;

            // Act
            StatWithMax stat = new StatWithMax(baseValue, maxValue);

            // Assert
            Assert.AreEqual(baseValue, stat.Value, "CurrentValue should be initialized to baseValue.");
            Assert.AreEqual(maxValue, stat.MaxValue, "MaxValue should be initialized correctly.");
        }

        [Test]
        public void UpdateBaseStat_IncreasesCurrentValue_WithoutExceedingMaxValue()
        {
            // Arrange
            float baseValue = 50f;
            float maxValue = 100f;
            StatWithMax stat = new StatWithMax(baseValue, maxValue);

            // Act
            stat.UpdateBaseStat(30f);

            // Assert
            Assert.AreEqual(80f, stat.Value, "CurrentValue should be increased by 30.");
        }

        [Test]
        public void UpdateBaseStat_ClampsCurrentValueToMaxValue_WhenExceeded()
        {
            // Arrange
            float baseValue = 50f;
            float maxValue = 100f;
            StatWithMax stat = new StatWithMax(baseValue, maxValue);

            // Act
            stat.UpdateBaseStat(60f);

            // Assert
            Assert.AreEqual(maxValue, stat.Value, "CurrentValue should be clamped to MaxValue.");
        }

        [Test]
        public void UpdateBaseStat_ClampsCurrentValueToZero_WhenNegative()
        {
            // Arrange
            float baseValue = 50f;
            float maxValue = 100f;
            StatWithMax stat = new StatWithMax(baseValue, maxValue);

            // Act
            stat.UpdateBaseStat(-60f);

            // Assert
            Assert.AreEqual(0f, stat.Value, "CurrentValue should be clamped to 0.");
        }

        [Test]
        public void UpdateBaseStat_IncreasesAndThenClampsCurrentValue()
        {
            // Arrange
            float baseValue = 50f;
            float maxValue = 100f;
            StatWithMax stat = new StatWithMax(baseValue, maxValue);

            // Act
            stat.UpdateBaseStat(30f); // Should be 80f
            stat.UpdateBaseStat(50f); // Should be clamped to 100f

            // Assert
            Assert.AreEqual(maxValue, stat.Value, "CurrentValue should be clamped to MaxValue after updates.");
        }
    }

