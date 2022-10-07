﻿using System.Threading.Tasks;

using NUnit.Framework;

using Xamarin.Forms.Mocks;
using Xamarin.Forms;

using Game;
using Game.Views;
using Game.Models;
using Game.ViewModels;
using System.Collections.Generic;

namespace UnitTests.Views
{
    [TestFixture]
    public class BattlePageTests : BattlePage
    {
        App app;
        BattlePage page;

        public BattlePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new BattlePage();

            // Put seed data into the system for all tests
            _ = BattleEngineViewModel.Instance.Engine.Round.ClearLists();

            //Start the Engine in AutoBattle Mode
            _ = BattleEngineViewModel.Instance.Engine.StartBattle(false);

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(new CharacterModel()));
            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Add(new PlayerInfoModel(new MonsterModel()));
            _ = BattleEngineViewModel.Instance.Engine.Round.MakePlayerList();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void BattlePage_OnAppearing_Should_Pass()
        {
            // Get the current valute
            InTestMode = true;
            // Act
            OnAppearing();

            // Reset
            InTestMode = false;
            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_OnAppearing_LoadingNewBattle_True_Should_Reset_To_False()
        {
            // Get the current valute
            InTestMode = true;
            this.LoadingNewBattle = true;

            // Act
            OnAppearing();

            // Reset
            InTestMode = false;
            // Assert
            Assert.AreEqual(false, page.LoadingNewBattle); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BattlePage_AttackButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            page.InTestMode = true;
            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(new CharacterModel(){
                MaxHealth = 100,
                CurrentHealth = 100}));

            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Clear();

            _ = BattleEngineViewModel.Instance.Engine.Round.MakePlayerList();

            // Act
            page.AttackButton_Clicked(null, null);
            //Reset
            page.InTestMode = false;
            //BattleEngineViewModel.Instance.Engine.EngineSettings.MaxTurnCount = 1000;

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_ShowScoreButton_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ShowScoreButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_ExitButton_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ExitButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_StartButton_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.StartButton_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_NextRoundButton_Clicked_Default_Should_Pass()
        {
            // Arrange
            page.InTestMode = true;
            // Act
            page.NextRoundButton_Clicked(null, null);

            // Reset
            page.InTestMode = false;
            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_ShowModalRoundOverPage_Default_Should_Pass()
        {
            // Arrange
            page.InTestMode = true;
            // Act
            page.ShowModalRoundOverPage();

            // Reset
            page.InTestMode = false;
            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


        [Test]
        public void BattlePage_ClearMessages_Default_Should_Pass()
        {
            // Arrange
            page.InTestMode = true;
            // Act
            page.ClearMessages();

            // Reset
            page.InTestMode = false;
            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_GameMessage_Default_Should_Pass()
        {
            // Arrange
            page.InTestMode = true;
            // Act
            page.GameMessage();

            // Reset
            page.InTestMode = false;
            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_GameMessage_LevelUp_Default_Should_Pass()
        {
            // Arrange
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.LevelUpMessage = "me";

            // Act
            page.GameMessage();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
        [Test]
        public void BattlePage_DrawGameBoardAttackerDefender_CurrentAttacker_Null_CurrentDefender_Null_Should_Pass()
        {
            // Arrange
            page.InTestMode = true;
            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(null);
            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(null);

            // Act
            page.DrawGameAttackerDefenderBoard();

            // Reset
            page.InTestMode = false;

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_DrawGameBoardAttackerDefender_CurrentAttacker_InValid_Null_Should_Pass()
        {
            // Arrange
            page.InTestMode = true;

            var PlayerInfo = new PlayerInfoModel(new CharacterModel());

            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(PlayerInfo);
            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(null);

            // Act
            page.DrawGameAttackerDefenderBoard();

            // Reset
            page.InTestMode = false;

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_DrawGameBoardAttackerDefender_CurrentDefender_InValid_Null_Should_Pass()
        {
            // Arrange

            page.InTestMode = true;
            var PlayerInfo = new PlayerInfoModel(new CharacterModel());

            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(null);
            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(PlayerInfo);

            // Act
            page.DrawGameAttackerDefenderBoard();

            // Reset
            page.InTestMode = false;
            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_DrawGameBoardAttackerDefender_CurrentDefender_Valid_Should_Pass()
        {
            // Arrange
            page.InTestMode = true;
            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(new PlayerInfoModel(new CharacterModel()));
            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(new PlayerInfoModel(new CharacterModel { Alive = false }));

            // Act
            page.DrawGameAttackerDefenderBoard();

            // Reset
            page.InTestMode = false;
            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_DrawGameBoardAttackerDefender_Invalid_AttackerSource_Null_Should_Pass()
        {
            // Arrange
            page.InTestMode = true;

            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(new PlayerInfoModel(new CharacterModel()));
            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(new PlayerInfoModel(new CharacterModel { Alive = false }));

            var oldItem = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PrimaryHand;

            var item = ItemIndexViewModel.Instance.GetDefaultItem(ItemLocationEnum.PrimaryHand);
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PrimaryHand = item.Id;

            // Act
            page.DrawGameAttackerDefenderBoard();

            // Reset
            page.InTestMode = false;
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PrimaryHand = oldItem;

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_NextAttackExample_NextRound_Should_Pass()
        {
            // Arrange
            page.InTestMode = true;
            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(new CharacterModel()));

            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Clear();

            _ = BattleEngineViewModel.Instance.Engine.Round.MakePlayerList();

            // Has no monster, so should show next round.

            // Act
            page.NextAttackExample();

            // Reset
            page.InTestMode = false;
            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_NextAttackExample_GameOver_Should_Pass()
        {
            // Arrange
            page.InTestMode = true;

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Clear();
            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Clear();
            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Clear();

            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Add(new PlayerInfoModel(new MonsterModel()));

            _ = BattleEngineViewModel.Instance.Engine.Round.MakePlayerList();

            // Has no Character, so should show end game

            // Act
            
            page.NextAttackExample();

            // Reset
            page.InTestMode = false;
            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_NextAttackExample_PlayerPickNextTurn_Should_Pass()
        {
            // Arrange
            page.InTestMode = true;
            var player1 = new PlayerInfoModel(new CharacterModel());
            var player2 = new PlayerInfoModel(new CharacterModel());
            var monster = new PlayerInfoModel(new MonsterModel());

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(player1);
            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(player2);

            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Add(monster);
            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(player1);
            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(player2);
            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(monster);
            _ = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.PopulateMapModel(BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList);

            _ = BattleEngineViewModel.Instance.Engine.Round.MakePlayerList();

            // Act
            page.NextAttackExample();

            // Reset
            page.InTestMode = false;
            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_SetAttackerAndDefender_Character_vs_Monster_Should_Pass()
        {
            // Arrange
            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Clear();
            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Clear();
            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Clear();

            // Make Character
            BattleEngineViewModel.Instance.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 100,
                                Level = 10,
                                CurrentHealth = 11,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                                ListOrder = 1,
                            });

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            // Make Monster

            BattleEngineViewModel.Instance.Engine.EngineSettings.MaxNumberPartyMonsters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                            new MonsterModel
                            {
                                Speed = -1,
                                Level = 10,
                                CurrentHealth = 11,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                                ListOrder = 1,
                            });

            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(CharacterPlayer);
            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(MonsterPlayer);

            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(MonsterPlayer);

            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(null);

            // Act
            page.SetAttackerAndDefender();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_SetAttackerAndDefender_Monster_vs_Character_Should_Pass()
        {
            // Arrange

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Clear();
            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Clear();
            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Clear();

            // Make Character
            BattleEngineViewModel.Instance.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1,
                                Level = 10,
                                CurrentHealth = 11,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                                ListOrder = 1,
                            });

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            // Make Monster

            BattleEngineViewModel.Instance.Engine.EngineSettings.MaxNumberPartyMonsters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                            new MonsterModel
                            {
                                Speed = 100,
                                Level = 10,
                                CurrentHealth = 11,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                                ListOrder = 1,
                            });

            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(CharacterPlayer);
            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(MonsterPlayer);

            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(CharacterPlayer);

            // Act
            page.SetAttackerAndDefender();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_SetAttackerAndDefender_Character_vs_Unknown_Should_Pass()
        {
            // Arrange
            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Clear();
            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Clear();
            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Clear();

            // Make Character
            BattleEngineViewModel.Instance.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1,
                                Level = 10,
                                CurrentHealth = 11,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                                ListOrder = 1,
                            });

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            // Make Monster

            BattleEngineViewModel.Instance.Engine.EngineSettings.MaxNumberPartyMonsters = 1;

            var MonsterPlayer = new PlayerInfoModel(
                            new MonsterModel
                            {
                                Speed = 100,
                                Level = 10,
                                CurrentHealth = 11,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                                ListOrder = 1,
                                PlayerType = PlayerTypeEnum.Unknown
                            });

            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(CharacterPlayer);
            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(MonsterPlayer);

            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(CharacterPlayer);

            // Act
            page.SetAttackerAndDefender();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_GameOver_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.GameOver();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_SetSelectedCharacter_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page.SetSelectedCharacter(new MapModelLocation());

            // Reset

            // Assert
            Assert.AreEqual(true, result); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_SetSelectedMonster_Default_Should_Pass()
        {
            // Arrange
            page.InTestMode = true;
            // Act
            var result = page.SetSelectedMonster(new MapModelLocation());

            // Reset
            page.InTestMode = false;
            // Assert
            Assert.AreEqual(true, result); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_SetSelectedEmpty_Default_Should_Pass()
        {
            // Arrange
            page.InTestMode = true;

            // Act
            var result = page.SetSelectedEmpty(new MapModelLocation());

            // Reset
            page.InTestMode = false;
            // Assert
            Assert.AreEqual(true, result); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_SetSelectedEmpty_Valid_AvailableLocations_Contain_Data_Should_Pass()
        {
            // Arrange
            page.InTestMode = true;
            var data = new MapModelLocation();
            data.Player = new PlayerInfoModel() { PlayerType = PlayerTypeEnum.Monster };
            page.AvailableLocations.Add(data);
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Move;
            // Act
            var result = page.SetSelectedEmpty(data);

            // Reset
            page.InTestMode = false;
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Unknown;

            // Assert
            Assert.AreEqual(true, result); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_UpdateMapGrid_InValid_Bogus_Image_Should_Fail()
        {
            // Make the Row Bogus
            page.InTestMode = true;
            BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation[0, 0].Row = -1;

            // Act
            var result = page.UpdateMapGrid();

            // Reset
            page.InTestMode = false;
            BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation[0, 0].Row = 0;

            // Assert
            Assert.AreEqual(false, result); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_UpdateMapGrid_InValid_Bogus_ImageButton_Should_Fail()
        {
            // Get the current valute
            page.InTestMode = true;
            var name = "MapR0C0ImageButton";
            _ = page.MapLocationObject.TryGetValue(name, out var data);
            _ = page.MapLocationObject.Remove(name);

            // Act
            var result = page.UpdateMapGrid();

            // Reset
            page.InTestMode = false;
            page.MapLocationObject.Add(name, data);

            // Assert
            Assert.AreEqual(false, result); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_UpdateMapGrid_InValid_Bogus_Stack_Should_Fail()
        {
            // Get the current valute
            page.InTestMode = true;
            var nameStack = "MapR0C0Stack";
            _ = page.MapLocationObject.TryGetValue(nameStack, out var dataStack);
            _ = page.MapLocationObject.Remove(nameStack);

            var nameImage = "MapR0C0ImageButton";
            _ = page.MapLocationObject.TryGetValue(nameImage, out var dataImage);

            _ = page.MapLocationObject.Remove(nameImage);

            var dataImageBogus = new ImageButton { AutomationId = "bogus" };
            page.MapLocationObject.Add(nameImage, dataImageBogus);

            // Act
            var result = page.UpdateMapGrid();

            // Reset
            page.InTestMode = false;
            _ = page.MapLocationObject.Remove(nameImage);
            page.MapLocationObject.Add(nameImage, dataImage);
            page.MapLocationObject.Add(nameStack, dataStack);

            // Assert
            Assert.AreEqual(false, result); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_UpdateMapGrid_Valid_Stack_Should_Pass()
        {
            // Need to build out a valid MapGrid with Engine MapGridLocation
            page.InTestMode = true;
            // Make Map in Engine

            var MonsterPlayer = new PlayerInfoModel(new MonsterModel());

            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(MonsterPlayer);

            _ = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.PopulateMapModel(BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList);

            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Unknown;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.AutoBattle = true;

            // Make UI Map
            _ = page.CreateMapGridObjects();
            _ = page.UpdateMapGrid();

            // Move Character in Engine
            var result = BattleEngineViewModel.Instance.Engine.Round.Turn.MoveAsTurn(MonsterPlayer);

            // Act

            // Call for UpateMap
            _ = page.UpdateMapGrid();

            // Reset
            page.InTestMode = false;

            // Assert
            Assert.AreEqual(false, result); // Got to here, so it happened...
        }

        [Test]
        public async Task BattlePage_ShowBattleSettingsPage_Default_Should_Pass()
        {
            // Get the current valute
            page.InTestMode = true;
            // Act
            await page.ShowBattleSettingsPage();

            // Reset
            page.InTestMode = false;
            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattlePage_Settings_Clicked_Default_Should_Pass()
        {
            // Get the current valute
            page.InTestMode = true;
            // Act
            page.Setttings_Clicked(null, null);

            // Reset
            page.InTestMode = false;
            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void BattleSettingsPage_MakeMapGridBox_InValid_Should_Fail()
        {
            // Arrange
            var data = new MapModelLocation { Player = null, Column = 0, Row = 0 };

            // Act
            var result = page.MakeMapGridBox(data);

            // Reset

            // Assert
            Assert.AreEqual(HitStatusEnum.Default, BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum);
        }

        [Test]
        public void BattleSettingsPage_ShowBattleMode_Default_Should_Pass()
        {
            // Arrange
            page.InTestMode = true;
            // Act
            page.ShowBattleMode();

            // Reset
            page.InTestMode = false;
            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_ShowBattleModeUIElements_Starting_Should_Pass()
        {
            // Arrange
            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Starting;

            // Act
            page.ShowBattleModeUIElements();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = save;

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_ShowBattleModeUIElements_NewRound_Should_Pass()
        {
            // Arrange
            page.InTestMode = true;
            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.NewRound;

            // Act
            page.ShowBattleModeUIElements();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = save;
            page.InTestMode = false;
            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_ShowBattleModeUIElements_GameOver_Should_Pass()
        {
            // Arrange
            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.GameOver;

            // Act
            page.ShowBattleModeUIElements();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = save;

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_ShowBattleModeUIElements_RoundOver_Should_Pass()
        {
            // Arrange
            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.RoundOver;

            // Act
            page.ShowBattleModeUIElements();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = save;

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_ShowBattleModeUIElements_Battling_Should_Pass()
        {
            // Arrange
            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            // Act
            page.ShowBattleModeUIElements();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = save;

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_ShowBattleModeUIElements_Unknown_Should_Pass()
        {
            // Arrange
            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Unknown;

            // Act
            page.ShowBattleModeUIElements();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = save;

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_ShowBattleModeDisplay_MapAbility_Should_Pass()
        {
            // Arrange
            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = BattleModeEnum.MapAbility;

            // Act
            page.ShowBattleModeDisplay();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = save;

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_ShowBattleModeDisplay_MapFull_Should_Pass()
        {
            // Arrange
            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = BattleModeEnum.MapFull;

            // Act
            page.ShowBattleModeDisplay();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = save;

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_ShowBattleModeDisplay_MapNext_Should_Pass()
        {
            // Arrange
            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = BattleModeEnum.MapNext;

            // Act
            page.ShowBattleModeDisplay();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = save;

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_ShowBattleModeDisplay_SimpleAbility_Should_Pass()
        {
            // Arrange
            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = BattleModeEnum.SimpleAbility;

            // Act
            page.ShowBattleModeDisplay();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = save;

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_ShowBattleModeDisplay_SimpleUnknown_Should_Pass()
        {
            // Arrange
            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = BattleModeEnum.Unknown;

            // Act
            page.ShowBattleModeDisplay();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = save;

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_ShowBattleModeDisplay_SimpleNext_Should_Pass()
        {
            // Arrange
            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum;
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = BattleModeEnum.SimpleNext;

            // Act
            page.ShowBattleModeDisplay();

            // Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum = save;

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_MapIcon_Clicked_Character_Should_Pass()
        {
            // Arrange
            var CharacterPlayer = new PlayerInfoModel(new CharacterModel());
            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(CharacterPlayer);

            var MonsterPlayer = new PlayerInfoModel(new MonsterModel());
            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(MonsterPlayer);

            _ = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.PopulateMapModel(BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList);

            // Make UI Map
            _ = page.CreateMapGridObjects();

            var nameImage = "MapR0C0ImageButton";
            _ = page.MapLocationObject.TryGetValue(nameImage, out var dataImage);

            // Act

            // Force the click event to fire
            ((ImageButton)dataImage).PropagateUpClicked();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_MapIcon_Clicked_Monster_Should_Pass()
        {
            // Arrange
            var CharacterPlayer = new PlayerInfoModel(new CharacterModel());
            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(CharacterPlayer);

            var MonsterPlayer = new PlayerInfoModel(new MonsterModel());
            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(MonsterPlayer);

            _ = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.PopulateMapModel(BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList);

            // Make UI Map
            _ = page.CreateMapGridObjects();

            var nameImage = "MapR0C0ImageButton";
            _ = page.MapLocationObject.TryGetValue(nameImage, out var dataImage);

            // Act

            // Force the click event to fire
            ((ImageButton)dataImage).PropagateUpClicked();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got Here
        }

        [Test]
        public void BattleSettingsPage_MapIcon_Clicked_Empty_Should_Pass()
        {
            // Arrange
            page.InTestMode = true;
            var CharacterPlayer = new PlayerInfoModel(new CharacterModel());
            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(CharacterPlayer);

            var MonsterPlayer = new PlayerInfoModel(new MonsterModel());
            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(MonsterPlayer);

            _ = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.PopulateMapModel(BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList);

            // Make UI Map
            page.DrawMapGridInitialState();

            var nameImage = "MapR3C3ImageButton";
            _ = page.MapLocationObject.TryGetValue(nameImage, out var dataImage);

            // Act

            // Force the click event to fire
            ((ImageButton)dataImage).PropagateUpClicked();

            // Reset
            page.InTestMode = false;
            // Assert
            Assert.IsTrue(true); // Got Here
        }

        #region ShowActionPopup
        [Test]
        public void BattlePage_ShowActionPopup_HitEnum_Hit_Should_Pass()
        {
            //Arrange
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Hit;

            //Act
            page.ShowActionPopup(ActionEnum.Attack);

            //Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Default;

            //Assert
            Assert.AreEqual(true, true);
        }

        [Test]
        public void BattlePage_ShowActionPopup_HitEnum_Miss_Should_Pass()
        {
            //Arrange
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Miss;

            //Act
            page.ShowActionPopup(ActionEnum.Attack);

            //Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Default;

            //Assert
            Assert.AreEqual(true, true);
        }

        [Test]
        public void BattlePage_ShowActionPopup_HitEnum_CriticalMiss_Should_Pass()
        {
            //Arrange
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.CriticalMiss;

            //Act
            page.ShowActionPopup(ActionEnum.Attack);

            //Reset
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Default;

            //Assert
            Assert.AreEqual(true, true);
        }

        [Test]
        public void BattlePage_ShowActionPopup_ActionEnum_Move_Should_Pass()
        {
            //Arrange
            

            //Act
            page.ShowActionPopup(ActionEnum.Move);

            //Reset
            
            //Assert
            Assert.AreEqual(true, true);
        }

        [Test]
        public void BattlePage_ShowActionPopup_ActionEnum_Unknown_Should_Pass()
        {
            //Arrange


            //Act
            page.ShowActionPopup(ActionEnum.Unknown);

            //Reset

            //Assert
            Assert.AreEqual(true, true);
        }

        [Test]
        public void BattlePage_DetermineMapImageButton_PlayerTypeEnum_Character_Should_Pass()
        {
            //Arrange
            InTestMode = true;
            var data = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation[0,0];
            var PlayerImageButton = DetermineMapImageButton(data);
            MapModelLocation data2 = new MapModelLocation();
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Attack;

            //Act
            PlayerImageButton.PropagateUpClicked();

            //Reset
            InTestMode = false;
            //Assert
            Assert.AreEqual(true, true);
        }

        [Test]
        public void BattlePage_DetermineMapImageButton_PlayerTypeEnum_Monster_Should_Pass()
        {
            //Arrange
            InTestMode = true;

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(new CharacterModel()));

            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Clear();

            _ = BattleEngineViewModel.Instance.Engine.Round.MakePlayerList();

            var save = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel;
            var player = new PlayerInfoModel(new MonsterModel());
            BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation[0, 0].Player = player;
            var location = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.GetLocationForPlayer(player);
            var PlayerImageButton = DetermineMapImageButton(location);
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Attack;
            
            //Act
            PlayerImageButton.PropagateUpClicked();

            //Reset
            InTestMode = false;
            BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel = save;
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Unknown;
            
            
            //Assert
            Assert.AreEqual(true, true);
        }

        [Test]
        public void BattlePage_DetermineMapImageButton_PlayerTypeEnum_Unkown_Should_Pass()
        {
            //Arrange
            InTestMode = true;
            var data = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation[0, 0];
            data.Player.PlayerType = PlayerTypeEnum.Unknown;
            var PlayerImageButton = DetermineMapImageButton(data);
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Attack;

            //Act
            PlayerImageButton.PropagateUpClicked();

            //Reset
            InTestMode = false;

            //Assert
            Assert.AreEqual(true, true);
        }
        #endregion

        #region Attack

        [Test]
        public void BattlePage_VisualizeAttackOptions_Default_Should_Pass()
        {
            //Arrange
            var monster1 = new PlayerInfoModel(new MonsterModel());
            var monster2 = new PlayerInfoModel(new MonsterModel());
            var player = new PlayerInfoModel(new CharacterModel()
            {
                PlayerType = PlayerTypeEnum.Character
            });

            // Arrange
            var map = new MapModel();

            map.MapXAxiesCount = 3;
            map.MapYAxiesCount = 3;
            map.MapGridLocation = new MapModelLocation[map.MapXAxiesCount, map.MapYAxiesCount];

            var PlayerList = new List<PlayerInfoModel>();
            PlayerList.Add(new PlayerInfoModel(player));
            PlayerList.Add(new PlayerInfoModel(monster1));

            _ = map.PopulateMapModel(PlayerList);
            var start = map.GetPlayerAtLocation(0, 0);
            var end = map.GetPlayerAtLocation(1, 0);

            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(monster1);
            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(monster2);

            //Act
            visualizeAttackOptions(player);

            //Reset

            //Assert
            Assert.AreEqual(true, true);
        }

        [Test]
        public void BattlePage_VisualizeAttackOptions_Valid_InRange_Should_Pass()
        {
            //Arrange
            var monster1 = new PlayerInfoModel(new MonsterModel());
            var monster2 = new PlayerInfoModel(new MonsterModel());
            var player = new PlayerInfoModel(new CharacterModel()
            {
                PlayerType = PlayerTypeEnum.Character
            });

            // Arrange
            var map = new MapModel();

            map.MapXAxiesCount = 3;
            map.MapYAxiesCount = 3;
            map.MapGridLocation = new MapModelLocation[map.MapXAxiesCount, map.MapYAxiesCount];

            var PlayerList = new List<PlayerInfoModel>();
            PlayerList.Add(new PlayerInfoModel(player));
            PlayerList.Add(new PlayerInfoModel(monster1));

            _ = map.PopulateMapModel(PlayerList);
            var start = new MapModelLocation() {
                Column = 0,
                Row = 0
            };
            //map.GetPlayerAtLocation(0, 0);
            var end = new MapModelLocation()
            {
                Column = 1,
                Row = 0
            };
            MapModelLocation playerLocation = new MapModelLocation()
            {
                Column = 0,
                Row = 0,
                Player = player,
                IsSelectedTarget = false
            };
            MapModelLocation monsterLocation = new MapModelLocation()
            {
                Column = 0,
                Row = 0,
                Player = player,
                IsSelectedTarget = false
            };
            BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MovePlayerOnMap(start, playerLocation);
            BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MovePlayerOnMap(end, monsterLocation);

            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(monster1);
            BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Add(monster2);

            //Act
            visualizeAttackOptions(player);

            //Reset

            //Assert
            Assert.AreEqual(true, true);
        }


        #endregion

        #region DetermineMapBackgroundColor
        [Test]
        public void BattlePage_DetermineMapBackgroundColor_PlayerTypeEnum_Character_Should_Pass()
        {
            //Arrange
            var data = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation[0, 0];
            data.Player.PlayerType = PlayerTypeEnum.Character;

            //Act
            var result = DetermineMapBackgroundColor(data);

            //Reset

            //Assert
            Assert.AreEqual(true, true);
        }

        [Test]
        public void BattlePage_DetermineMapBackgroundColor_PlayerTypeEnum_Monster_Should_Pass()
        {
            //Arrange
            var data = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation[0, 0];
            data.Player.PlayerType = PlayerTypeEnum.Monster;

            //Act
            var result = DetermineMapBackgroundColor(data);

            //Reset

            //Assert
            Assert.AreEqual(true, true);
        }

        [Test]
        public void BattlePage_DetermineMapBackgroundColor_PlayerTypeEnum_Unknown_Should_Pass()
        {
            //Arrange
            var data = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation[0, 0];
            data.Player.PlayerType = PlayerTypeEnum.Unknown;

            //Act
            var result = DetermineMapBackgroundColor(data);

            //Reset

            //Assert
            Assert.AreEqual(true, true);
        }

        #endregion

        #region GetDictionaryFramename
        [Test]
        public void BattlePage_GetDictionaryFrameName_Default_Should_Pass()
        {
            //Arrange
            var data = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation[0, 0];
            data.Player.PlayerType = PlayerTypeEnum.Character;

            //Act
            var result = GetDictionaryFrameName(data);

            //Reset

            //Assert
            Assert.AreEqual(true, true);
        }
        #endregion

        #region SkipTurn

        [Test]
        public void BattlePage_SkipTurn_Default_Should_Pass()
        {
            //Arrange
            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(new CharacterModel()));

            BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Clear();

            _ = BattleEngineViewModel.Instance.Engine.Round.MakePlayerList();

            page.InTestMode = true;
            //Act
            page.skipTurn(null, null);

            //Reset
            page.InTestMode = false;
            //Assert
            Assert.AreEqual(true, true);
        }

        #endregion

        #region OnAppearing

        #endregion
    }
}