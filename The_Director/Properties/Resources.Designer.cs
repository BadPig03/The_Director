﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace The_Director.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("The_Director.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性，对
        ///   使用此强类型资源类的所有资源查找执行重写。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 //-----------------------------------------------------
        /////
        /////
        /////-----------------------------------------------------
        ///Msg(&quot;Initiating c10m5_houseboat_finale script\n&quot;);
        ///
        /////-----------------------------------------------------
        ///ERROR		&lt;- -1
        ///PANIC 		&lt;- 0
        ///TANK 		&lt;- 1
        ///DELAY 		&lt;- 2
        ///SCRIPTED 	&lt;- 3
        /////-----------------------------------------------------
        ///
        ///StageDelay &lt;- 0
        ///PreEscapeDelay &lt;- 0
        ///if ( Director.GetGameModeBase() == &quot;coop&quot; || Director.GetGameModeBase() == &quot;realism&quot; )
        ///{
        ///	StageDelay &lt;- 5
        ///	P [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string c10m5_houseboat_finale {
            get {
                return ResourceManager.GetString("c10m5_houseboat_finale", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 //-----------------------------------------------------
        /////
        /////
        /////-----------------------------------------------------
        ///Msg(&quot;Initiating c11m5_runway_finale script\n&quot;);
        ///
        /////-----------------------------------------------------
        ///ERROR		&lt;- -1
        ///PANIC 		&lt;- 0
        ///TANK 		&lt;- 1
        ///DELAY 		&lt;- 2
        ///SCRIPTED 	&lt;- 3
        /////-----------------------------------------------------
        ///
        ///StageDelay &lt;- 0
        ///PreEscapeDelay &lt;- 0
        ///if ( Director.GetGameModeBase() == &quot;coop&quot; || Director.GetGameModeBase() == &quot;realism&quot; )
        ///{
        ///	StageDelay &lt;- 5
        ///	PreE [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string c11m5_runway_finale {
            get {
                return ResourceManager.GetString("c11m5_runway_finale", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 //-----------------------------------------------------
        /////
        /////
        /////-----------------------------------------------------
        ///Msg(&quot;Initiating c12m5_cornfield_finale script\n&quot;);
        ///
        /////-----------------------------------------------------
        ///ERROR		&lt;- -1
        ///PANIC 		&lt;- 0
        ///TANK 		&lt;- 1
        ///DELAY 		&lt;- 2
        ///SCRIPTED 	&lt;- 3
        /////-----------------------------------------------------
        ///
        ///StageDelay &lt;- 0
        ///PreEscapeDelay &lt;- 0
        ///if ( Director.GetGameModeBase() == &quot;coop&quot; || Director.GetGameModeBase() == &quot;realism&quot; )
        ///{
        ///	StageDelay &lt;- 5
        ///	P [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string c12m5_cornfield_finale {
            get {
                return ResourceManager.GetString("c12m5_cornfield_finale", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Msg(&quot;Initiating c14m2_lighthouse_finale script\n&quot;);
        ///
        ///StageDelay &lt;- 15
        ///PreEscapeDelay &lt;- 10
        ///
        /////-----------------------------------------------------
        ///PANIC &lt;- 0
        ///TANK &lt;- 1
        ///DELAY &lt;- 2
        ///ONSLAUGHT &lt;- 3
        /////-----------------------------------------------------
        ///
        ///DirectorOptions &lt;-
        ///{
        ///	A_CustomFinale_StageCount = 8
        ///	
        ///	A_CustomFinale1 		= PANIC
        ///	A_CustomFinaleValue1 	= 2
        ///	A_CustomFinale2 		= DELAY
        ///	A_CustomFinaleValue2 	= StageDelay
        ///	A_CustomFinale3 		= TANK
        ///	A_CustomFinaleValue3 	= 1
        ///	A_CustomFinal [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string c14m2_lighthouse_finale {
            get {
                return ResourceManager.GetString("c14m2_lighthouse_finale", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Msg(&quot;----------------------FINALE SCRIPT------------------\n&quot;)
        /////-----------------------------------------------------
        ///PANIC &lt;- 0
        ///TANK &lt;- 1
        ///DELAY &lt;- 2
        ///ONSLAUGHT &lt;- 3
        /////-----------------------------------------------------
        ///
        ///SharedOptions &lt;-
        ///{
        /// 	A_CustomFinale1 = ONSLAUGHT
        ///	A_CustomFinaleValue1 = &quot;&quot;
        ///
        ///	A_CustomFinale2 = PANIC
        ///	A_CustomFinaleValue2 = 1
        ///
        ///	A_CustomFinale3 = ONSLAUGHT
        ///	A_CustomFinaleValue3 = &quot;c1m4_delay&quot;
        ///        
        ///	A_CustomFinale4 = PANIC
        ///	A_CustomFinaleValue4 = 1
        ///
        ///	A_CustomFi [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string c1m4_atrium_finale {
            get {
                return ResourceManager.GetString("c1m4_atrium_finale", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Msg(&quot;**Delay started**\n&quot;)
        ///
        ///DirectorOptions &lt;-
        ///{
        ///	MobMinSize = 2
        ///	MobMaxSize = 3
        ///        
        ///	BoomerLimit = 0
        ///	SmokerLimit = 0
        ///	HunterLimit = 0
        ///	SpitterLimit = 0
        ///	JockeyLimit = 0
        ///	ChargerLimit = 0
        ///        
        ///	MinimumStageTime = 15
        ///       
        ///	CommonLimit = 5
        ///}
        ///
        ///Director.ResetMobTimer()
        ///
        ///
        ///// start the delay timer
        ///EntFire( &quot;timer_delay_end&quot;, &quot;enable&quot; )
        ///
        /////reset
        ///DelayTouchedOrPoured   &lt;- 0
        ///DelayPoured            &lt;- 0
        /////-------------------------------------------------
        ///
        ///
        ///// abort the dela [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string c1m4_delay {
            get {
                return ResourceManager.GetString("c1m4_delay", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 //-----------------------------------------------------------------------------
        ///
        ///PANIC &lt;- 0
        ///TANK &lt;- 1
        ///DELAY &lt;- 2
        ///ONSLAUGHT &lt;- 3
        ///
        /////-----------------------------------------------------------------------------
        ///
        ///SharedOptions &lt;-
        ///{
        ///	A_CustomFinale_StageCount = 9
        ///	
        /// 	A_CustomFinale1 = PANIC
        ///	A_CustomFinaleValue1 = 1
        ///	
        /// 	A_CustomFinale2 = PANIC
        ///	A_CustomFinaleValue2 = 1
        ///
        ///	A_CustomFinale3 = DELAY
        ///	A_CustomFinaleValue3 = 15
        ///
        ///	A_CustomFinale4 = TANK
        ///	A_CustomFinaleValue4 = 1
        ///	A_CustomFinaleM [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string c2m5_concert_finale {
            get {
                return ResourceManager.GetString("c2m5_concert_finale", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 //-----------------------------------------------------
        ///local PANIC = 0
        ///local TANK = 1
        ///local DELAY = 2
        /////-----------------------------------------------------
        ///
        ///DirectorOptions &lt;-
        ///{
        ///	//-----------------------------------------------------
        ///
        ///	 A_CustomFinale_StageCount = 8
        ///	 
        ///	 A_CustomFinale1 = PANIC
        ///	 A_CustomFinaleValue1 = 2
        ///	 
        ///	 A_CustomFinale2 = DELAY
        ///	 A_CustomFinaleValue2 = 12
        ///	 
        ///	 A_CustomFinale3 = TANK
        ///	 A_CustomFinaleValue3 = 1
        ///	 
        ///	 A_CustomFinale4 = DELAY
        ///	 A_CustomFinaleValue4 [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string c3m4_plantation_finale {
            get {
                return ResourceManager.GetString("c3m4_plantation_finale", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 //-----------------------------------------------------
        ///local PANIC = 0
        ///local TANK = 1
        ///local DELAY = 2
        /////-----------------------------------------------------
        ///
        ///// default finale patten - for reference only
        ///
        ////*
        ///CustomFinale1 &lt;- PANIC
        ///CustomFinaleValue1 &lt;- 2
        ///
        ///CustomFinale2 &lt;- DELAY
        ///CustomFinaleValue2 &lt;- 10
        ///
        ///CustomFinale3 &lt;- TANK
        ///CustomFinaleValue3 &lt;- 1
        ///
        ///CustomFinale4 &lt;- DELAY
        ///CustomFinaleValue4 &lt;- 10
        ///
        ///CustomFinale5 &lt;- PANIC
        ///CustomFinaleValue5 &lt;- 2
        ///
        ///CustomFinale6 &lt;- DELAY
        ///CustomFinaleV [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string c4m5_milltown_escape_finale {
            get {
                return ResourceManager.GetString("c4m5_milltown_escape_finale", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Msg(&quot;----------------------FINALE SCRIPT------------------\n&quot;)
        /////-----------------------------------------------------
        ///PANIC &lt;- 0
        ///TANK &lt;- 1
        ///DELAY &lt;- 2
        ///ONSLAUGHT &lt;- 3
        /////-----------------------------------------------------
        ///
        ///SharedOptions &lt;-
        ///{
        /// 	A_CustomFinale1 = ONSLAUGHT
        ///	A_CustomFinaleValue1 = &quot;&quot;
        ///
        ///	A_CustomFinale2 = PANIC
        ///	A_CustomFinaleValue2 = 1
        ///
        ///	A_CustomFinale3 = ONSLAUGHT
        ///	A_CustomFinaleValue3 = &quot;c1m4_delay&quot;
        ///        
        ///	A_CustomFinale4 = PANIC
        ///	A_CustomFinaleValue4 = 1
        ///
        ///	A_CustomFi [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string c6m3_port_finale {
            get {
                return ResourceManager.GetString("c6m3_port_finale", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 //-----------------------------------------------------
        ///// This script handles the logic for the Port / Bridge
        ///// finale in the River Campaign. 
        /////
        /////-----------------------------------------------------
        ///Msg(&quot;Initiating c7m3_port_finale script\n&quot;);
        ///
        /////-----------------------------------------------------
        ///ERROR		&lt;- -1
        ///PANIC 		&lt;- 0
        ///TANK 		&lt;- 1
        ///DELAY 		&lt;- 2
        ///
        /////-----------------------------------------------------
        ///
        ///// This keeps track of the number of times the generator button has been pressed.  [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string c7m3_port_finale {
            get {
                return ResourceManager.GetString("c7m3_port_finale", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 //-----------------------------------------------------
        /////
        /////
        /////-----------------------------------------------------
        ///Msg(&quot;Initiating c8m5_rooftop_finale script\n&quot;);
        ///
        /////-----------------------------------------------------
        ///ERROR		&lt;- -1
        ///PANIC 		&lt;- 0
        ///TANK 		&lt;- 1
        ///DELAY 		&lt;- 2
        ///SCRIPTED 	&lt;- 3
        /////-----------------------------------------------------
        ///
        ///StageDelay &lt;- 0
        ///PreEscapeDelay &lt;- 0
        ///if ( Director.GetGameModeBase() == &quot;coop&quot; || Director.GetGameModeBase() == &quot;realism&quot; )
        ///{
        ///	StageDelay &lt;- 5
        ///	PreE [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string c8m5_rooftop_finale {
            get {
                return ResourceManager.GetString("c8m5_rooftop_finale", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 //-----------------------------------------------------
        /////
        /////
        /////-----------------------------------------------------
        ///Msg(&quot;Initiating c9m2_lots_finale script\n&quot;);
        ///
        /////-----------------------------------------------------
        ///ERROR		&lt;- -1
        ///PANIC 		&lt;- 0
        ///TANK 		&lt;- 1
        ///DELAY 		&lt;- 2
        ///SCRIPTED 	&lt;- 3
        /////-----------------------------------------------------
        ///
        ///StageDelay &lt;- 0
        ///PreEscapeDelay &lt;- 0
        ///if ( Director.GetGameModeBase() == &quot;coop&quot; || Director.GetGameModeBase() == &quot;realism&quot; )
        ///{
        ///	StageDelay &lt;- 5
        ///	PreEsca [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string c9m2_lots_finale {
            get {
                return ResourceManager.GetString("c9m2_lots_finale", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Msg(&quot;Initiating Gauntlet\n&quot;);
        ///
        ///DirectorOptions &lt;-
        ///{
        ///	PanicForever = true
        ///	PausePanicWhenRelaxing = true
        ///
        ///	IntensityRelaxThreshold = 0.99
        ///	RelaxMinInterval = 25
        ///	RelaxMaxInterval = 35
        ///	RelaxMaxFlowTravel = 400
        ///
        ///	LockTempo = 0
        ///	SpecialRespawnInterval = 20
        ///	PreTankMobMax = 20
        ///	ZombieSpawnRange = 3000
        ///	ZombieSpawnInFog = true
        ///
        ///	MobSpawnSize = 5
        ///	CommonLimit = 5
        ///
        ///	GauntletMovementThreshold = 500.0
        ///	GauntletMovementTimerLength = 5.0
        ///	GauntletMovementBonus = 2.0
        ///	GauntletMovementBonusMax =  [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string director_gauntlet {
            get {
                return ResourceManager.GetString("director_gauntlet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 zvrt/hAAAAAOAAAA/NXSAgAAAAFjMXN0cmVldHMAAACQFgAAOoIBAAAAACAAUCrFAACWROQxBEMAwCjFAECcROUxBEPlMQRD6zEEQwEAAAAikgEAAgAAAGR+AQChhgEAAAAAAAIAAAAOkgEAH38BAAAAAAAAAAAAAAAAAAAAAAAA8EIAAPBC6hYlP+oWJT9K8uc+SvLnPkIAAAAAAAAAAAAikgEA95ABAAEAAAAA4F3FAACvRMNzO0MAUFzFACCyRMUTMkPFEzJDw3M7QwAAAAABAAAAAJEBAAEAAAALfwEAAAAAAAEAAAAAABhdxQCQsETDcztDCAAAAAAAAAAAAAAAAAAAAADwQgAA8EJscPI9bHDyPWxw8j1scPI9QAAAAAAAAAAAAACRAQA2wQMAAAAAAAAQi8UAIMtEhCnGQgBIisUAQM5ENNK4QjbIwkKA+7xCAQAAADTBAwABAAAANcEDAAAAAAAAAAAAAQEAAAAArIrFALDMRID7vEIIAAAA [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string FinaleGauntletScriptNav {
            get {
                return ResourceManager.GetString("FinaleGauntletScriptNav", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 versioninfo
        ///{
        ///	&quot;editorversion&quot; &quot;400&quot;
        ///	&quot;editorbuild&quot; &quot;8864&quot;
        ///	&quot;mapversion&quot; &quot;276&quot;
        ///	&quot;formatversion&quot; &quot;100&quot;
        ///	&quot;prefab&quot; &quot;0&quot;
        ///}
        ///visgroups
        ///{
        ///}
        ///viewsettings
        ///{
        ///	&quot;bSnapToGrid&quot; &quot;1&quot;
        ///	&quot;bShowGrid&quot; &quot;1&quot;
        ///	&quot;bShowLogicalGrid&quot; &quot;0&quot;
        ///	&quot;nGridSpacing&quot; &quot;1&quot;
        ///}
        ///palette_plus
        ///{
        ///	&quot;color0&quot; &quot;255 255 255&quot;
        ///	&quot;color1&quot; &quot;255 255 255&quot;
        ///	&quot;color2&quot; &quot;255 255 255&quot;
        ///	&quot;color3&quot; &quot;255 255 255&quot;
        ///	&quot;color4&quot; &quot;255 255 255&quot;
        ///	&quot;color5&quot; &quot;255 255 255&quot;
        ///	&quot;color6&quot; &quot;255 255 255&quot;
        ///	&quot;color7&quot; &quot;255 255 255&quot;
        ///	&quot;color8&quot; &quot;255 255 255&quot;
        ///	&quot;color9&quot; &quot;255 255 255&quot; [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string FinaleGauntletScriptVmf {
            get {
                return ResourceManager.GetString("FinaleGauntletScriptVmf", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似  的本地化字符串。
        /// </summary>
        internal static string FinaleSacrificeScriptNav {
            get {
                return ResourceManager.GetString("FinaleSacrificeScriptNav", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似  的本地化字符串。
        /// </summary>
        internal static string FinaleSacrificeScriptVmf {
            get {
                return ResourceManager.GetString("FinaleSacrificeScriptVmf", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 zvrt/hAAAAAOAAAAiEr0AAEAAAFkZWZhdWx0AAAAVAQAAAEAAAAAAAAgALi6RQBYmEUAIEBCAFjKRQD4p0UAIEBCAiBAQgAgQEIAAAAAAgAAAAgAAAA9BQAAAAAAAAMAAACnBQAAHQAAACkAAAABAAAAAAD0yUUAvJhFAiBAQgEAAAAAAAAAAAAAAAAAAAAA8EIAAPBCBuUbPpqZmT2amZk9mpmZPUAAAAAAALQAAAAAAAAAAgQAAAABBQAAAAIHAAAAAgoAAAABDwAAAAETAAAAARQAAAABGwAAAAIcAAAAAh0AAAACJQAAAAEmAAAAASgAAAACLQAAAAMuAAAAAi8AAAABMAAAAAEzAAAAATQAAAABPwAAAANDAAAAAUYAAAABRwAAAAFMAAAAAU8AAAACUQAAAAFUAAAAAV8AAAADYAAAAAFiAAAAAmUAAAADZwAAAAFsAAAAAXMAAAACdAAAAAKIAAAAAooAAAABjgAAAAKUAAAAA54AAAABnwAA [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string FinaleScavengeScriptNav {
            get {
                return ResourceManager.GetString("FinaleScavengeScriptNav", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 versioninfo
        ///{
        ///	&quot;editorversion&quot; &quot;400&quot;
        ///	&quot;editorbuild&quot; &quot;8864&quot;
        ///	&quot;mapversion&quot; &quot;447&quot;
        ///	&quot;formatversion&quot; &quot;100&quot;
        ///	&quot;prefab&quot; &quot;0&quot;
        ///}
        ///visgroups
        ///{
        ///}
        ///viewsettings
        ///{
        ///	&quot;bSnapToGrid&quot; &quot;1&quot;
        ///	&quot;bShowGrid&quot; &quot;1&quot;
        ///	&quot;bShowLogicalGrid&quot; &quot;0&quot;
        ///	&quot;nGridSpacing&quot; &quot;1&quot;
        ///}
        ///palette_plus
        ///{
        ///	&quot;color0&quot; &quot;255 255 255&quot;
        ///	&quot;color1&quot; &quot;255 255 255&quot;
        ///	&quot;color2&quot; &quot;255 255 255&quot;
        ///	&quot;color3&quot; &quot;255 255 255&quot;
        ///	&quot;color4&quot; &quot;255 255 255&quot;
        ///	&quot;color5&quot; &quot;255 255 255&quot;
        ///	&quot;color6&quot; &quot;255 255 255&quot;
        ///	&quot;color7&quot; &quot;255 255 255&quot;
        ///	&quot;color8&quot; &quot;255 255 255&quot;
        ///	&quot;color9&quot; &quot;255 255 255&quot; [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string FinaleScavengeScriptVmf {
            get {
                return ResourceManager.GetString("FinaleScavengeScriptVmf", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 zvrt/hAAAAAOAAAArNeWAAEAAAFkZWZhdWx0AAAAygMAAAEAAAAAAAAgAIDUQwAAL0MABIxDAIA7RAAA+kMABIxDAASMQwAEjEMEAAAA4gAAAFAAAABAAAAAagAAAAIAAACZAAAAmgAAAAMAAADjAwAA5QMAAOQAAAADAAAAkgAAAOAAAAAzAAAAAQAAAAAAwNpDAMDzQwAEjEMIAAAAAAAAAAAAAAAAAAAAAPBCAADwQohZJT/OKCY/ny3UPR06rT5AAAAAAABeAQAAxAMAAAK/AwAAAsIDAAACtwMAAALIAwAAArQDAAACswMAAAK5AwAAAr4DAAACAAAAAAIBAAAAAwIAAAABDAAAAAMVAAAAARYAAAADFwAAAAEbAAAAARwAAAABHQAAAAEhAAAAASIAAAABIwAAAAEkAAAAASUAAAABLAAAAAEvAAAAAzIAAAACMwAAAAE1AAAAATYAAAACNwAAAAI4AAAAAjkAAAABPgAAAAM/AAAAAkAAAAAC [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string FinaleStandardScriptNav {
            get {
                return ResourceManager.GetString("FinaleStandardScriptNav", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 versioninfo
        ///{
        ///	&quot;editorversion&quot; &quot;400&quot;
        ///	&quot;editorbuild&quot; &quot;9520&quot;
        ///	&quot;mapversion&quot; &quot;586&quot;
        ///	&quot;formatversion&quot; &quot;100&quot;
        ///	&quot;prefab&quot; &quot;0&quot;
        ///}
        ///visgroups
        ///{
        ///	visgroup
        ///	{
        ///		&quot;name&quot; &quot;1204 objects&quot;
        ///		&quot;visgroupid&quot; &quot;78&quot;
        ///		&quot;color&quot; &quot;127 156 205&quot;
        ///	}
        ///	visgroup
        ///	{
        ///		&quot;name&quot; &quot;291 objects&quot;
        ///		&quot;visgroupid&quot; &quot;123&quot;
        ///		&quot;color&quot; &quot;106 147 80&quot;
        ///	}
        ///}
        ///viewsettings
        ///{
        ///	&quot;bSnapToGrid&quot; &quot;1&quot;
        ///	&quot;bShowGrid&quot; &quot;1&quot;
        ///	&quot;bShowLogicalGrid&quot; &quot;0&quot;
        ///	&quot;nGridSpacing&quot; &quot;1&quot;
        ///	&quot;bShow3DGrid&quot; &quot;0&quot;
        ///}
        ///world
        ///{
        ///	&quot;id&quot; &quot;1&quot;
        ///	&quot;mapversion&quot; &quot;586&quot;
        ///	&quot;classname&quot; &quot;worldspawn [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string FinaleStandardScriptVmf {
            get {
                return ResourceManager.GetString("FinaleStandardScriptVmf", resourceCulture);
            }
        }
    }
}
