namespace TestStarsHostEditor
{
    public class TestHostEditor
    {
        [Fact]
        public void TestLoadingOk()
        {
            var editor = new AtlantisSoftware.StarsHostEditor();
            editor.Load("TestFiles\\LoadsOk\\GAME.hst");
            foreach (var planetObj in editor.Planets())
            {
                if (planetObj is AtlantisSoftware.Planet planet)
                {
                    if (planet.OwnerID == 0)
                    {
                        switch(planet.PlanetID)
                        {
                            case 10:
                                Assert.Equal("Corvus", planet.Name);
                                break;
                            case 18:
                                Assert.Equal("Mana", planet.Name);
                                break;
                        }                        
                        Assert.Equal(11000, planet.Population);
                    }
                }
            }
        }

        [Fact]
        public void TestIndexOutOfRange()
        {
            var editor = new AtlantisSoftware.StarsHostEditor();
            editor.Load("TestFiles\\IndexOutOfRange\\GAME.hst");
            foreach (var planetObj in editor.Planets())
            {
                if (planetObj is AtlantisSoftware.Planet planet)
                {
                    if (planet.OwnerID == 0)
                    {
                        switch (planet.PlanetID)
                        {
                            case 10:
                                Assert.Equal("Lopsided", planet.Name);
                                Assert.Equal(175, planet.Population);
                                break;
                        }
                        
                    }
                }
            }
        }
    }    
}