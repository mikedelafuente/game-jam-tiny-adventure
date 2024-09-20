namespace TinyAdventure.Globals;

public static class KnownTileSets
{
    public static class PlayerSet
    {
        public static readonly TileSetAlias SetDetails = new TileSetAlias(0, "player1");
        public static readonly string AssetPath = "assets/textures/character/character_asset_pack.xml";
        public static readonly char SetDelimiter = '-';

        public static class Tiles
        {
            public static readonly TileAlias AirSpin = new TileAlias(0, "player-air-spin", SetDetails, AnimationStrategy.ForwardSingle, 12f);
            public static readonly TileAlias ClimbBack = new TileAlias(1, "player-climb-back", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CrouchIdle = new TileAlias(2, "player-crouch-idle", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CrouchWalk = new TileAlias(3, "player-crouch-walk", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias Dash = new TileAlias(4, "player-dash", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias Death = new TileAlias(5, "player-death", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias Fall = new TileAlias(9, "player-new-jump", SetDetails, AnimationStrategy.ForwardSingle, 7f, 3, 5);
            public static readonly TileAlias Hurt = new TileAlias(6, "player-hurt", SetDetails, AnimationStrategy.ForwardSingle, 8f);
            public static readonly TileAlias Idle = new TileAlias(7, "player-idle", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias Jab = new TileAlias(8, "player-jab", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias Jump = new TileAlias(9, "player-new-jump", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias KatanaAirAttackUpperBody = new TileAlias(10, "player-katana-air-attack-upper-body", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias KatanaAttackSheathe = new TileAlias(11, "player-katana-attack-sheathe", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias KatanaContinuousAttack = new TileAlias(12, "player-katana-continuous-attack", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias KatanaRun = new TileAlias(0, "player-katana-run", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias KatanaRunningAttackUpperBody = new TileAlias(13, "player-katana-running-attack-upper-body", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias Land = new TileAlias(14, "player-land", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias LedgeClimb = new TileAlias(15, "player-ledge-climb", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias Pull = new TileAlias(16, "player-pull", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias Punch = new TileAlias(17, "player-punch", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias PunchCross = new TileAlias(18, "player-punch-cross", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias Push = new TileAlias(19, "player-push", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias PushIdle = new TileAlias(20, "player-push-idle", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias Roll = new TileAlias(21, "player-roll", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias Run = new TileAlias(22, "player-run", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias RunningAiming = new TileAlias(23, "player-running-aiming", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias RunningShooting = new TileAlias(24, "player-running-shooting", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias Shoot2H = new TileAlias(25, "player-shoot-2h", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias SideClimb = new TileAlias(26, "player-side-climb", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias Slide = new TileAlias(27, "player-slide", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias SwordAttack = new TileAlias(28, "player-sword-attack", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias SwordIdle = new TileAlias(29, "player-sword-idle", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias SwordRun = new TileAlias(30, "player-sword-run", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias SwordStab = new TileAlias(31, "player-sword-stab", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias Walk = new TileAlias(32, "player-walk", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias WallLand = new TileAlias(33, "player-wall-land", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias WallLandLeft = new TileAlias(34, "player-wall-land-left", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias WallSlide = new TileAlias(35, "player-wall-slide", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias WallSlideLeft = new TileAlias(36, "player-wall-slide-left", SetDetails, AnimationStrategy.Forward, 12f);
        }
    }

    public static class SuperBasicTileSet
    {
        public static readonly TileSetAlias SetDetails = new TileSetAlias(0, "tile_set_basic1");
        public static readonly string AssetPath = "assets/textures/objects/super-basic-tilemap/super_basic.xml";
        public static readonly char SetDelimiter = '-';

        public static class Tiles
        {
            public static readonly TileAlias IslandR0C0 = new TileAlias(0, "island_r0_c0", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias IslandR0C1 = new TileAlias(1, "island_r0_c1", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias IslandR0C2 = new TileAlias(2, "island_r0_c2", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias IslandR0C3 = new TileAlias(3, "island_r0_c3", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias IslandR1C0 = new TileAlias(4, "island_r1_c0", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias IslandR1C1 = new TileAlias(5, "island_r1_c1", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias IslandR1C2 = new TileAlias(6, "island_r1_c2", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias IslandR1C3 = new TileAlias(7, "island_r1_c3", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias IslandR2C0 = new TileAlias(8, "island_r2_c0", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias IslandR2C1 = new TileAlias(9, "island_r2_c1", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias IslandR2C2 = new TileAlias(10, "island_r2_c2", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias IslandR2C3 = new TileAlias(11, "island_r2_c3", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias IslandR3C0 = new TileAlias(12, "island_r3_c0", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias IslandR3C1 = new TileAlias(13, "island_r3_c1", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias IslandR3C2 = new TileAlias(14, "island_r3_c2", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias IslandR3C3 = new TileAlias(15, "island_r3_c3", SetDetails, AnimationStrategy.Forward, 12f);

            public static readonly TileAlias CavernR0C0 = new TileAlias(16, "cavern_r0_c0", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR0C1 = new TileAlias(17, "cavern_r0_c1", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR0C2 = new TileAlias(18, "cavern_r0_c2", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR0C3 = new TileAlias(19, "cavern_r0_c3", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR0C4 = new TileAlias(20, "cavern_r0_c4", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR0C5 = new TileAlias(21, "cavern_r0_c5", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR1C0 = new TileAlias(22, "cavern_r1_c0", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR1C1 = new TileAlias(23, "cavern_r1_c1", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR1C2 = new TileAlias(24, "cavern_r1_c2", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR1C3 = new TileAlias(25, "cavern_r1_c3", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR1C4 = new TileAlias(26, "cavern_r1_c4", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR1C5 = new TileAlias(27, "cavern_r1_c5", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR2C0 = new TileAlias(28, "cavern_r2_c0", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR2C1 = new TileAlias(29, "cavern_r2_c1", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR2C2 = new TileAlias(30, "cavern_r2_c2", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR2C3 = new TileAlias(31, "cavern_r2_c3", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR2C4 = new TileAlias(32, "cavern_r2_c4", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR2C5 = new TileAlias(33, "cavern_r2_c5", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR3C0 = new TileAlias(34, "cavern_r3_c0", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR3C1 = new TileAlias(35, "cavern_r3_c1", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR3C2 = new TileAlias(36, "cavern_r3_c2", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR3C3 = new TileAlias(37, "cavern_r3_c3", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR3C4 = new TileAlias(38, "cavern_r3_c4", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR3C5 = new TileAlias(39, "cavern_r3_c5", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR4C0 = new TileAlias(40, "cavern_r4_c0", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR4C1 = new TileAlias(41, "cavern_r4_c1", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR4C2 = new TileAlias(42, "cavern_r4_c2", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR4C3 = new TileAlias(43, "cavern_r4_c3", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR4C4 = new TileAlias(44, "cavern_r4_c4", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR4C5 = new TileAlias(45, "cavern_r4_c5", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR5C0 = new TileAlias(46, "cavern_r5_c0", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR5C1 = new TileAlias(47, "cavern_r5_c1", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR5C2 = new TileAlias(48, "cavern_r5_c2", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR5C3 = new TileAlias(49, "cavern_r5_c3", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR5C4 = new TileAlias(50, "cavern_r5_c4", SetDetails, AnimationStrategy.Forward, 12f);
            public static readonly TileAlias CavernR5C5 = new TileAlias(51, "cavern_r5_c5", SetDetails, AnimationStrategy.Forward, 12f);
        }
    }
}
