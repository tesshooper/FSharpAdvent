﻿#r @"C:\Users\MukundRaghavSharma\Desktop\F#\FSharpAdvent\packages\FSharp.Data.2.4.3\lib\net45\FSharp.Data.dll"
#load "MovieCommon.fsx"

open System
open System.IO

open FSharp.Data

(* Entire Series - Overall Numbers *)

[<Literal>]
let lotrFilmSeries           = @"https://en.wikipedia.org/wiki/The_Lord_of_the_Rings_(film_series)"

type LotrFilmSeriesProvider  = HtmlProvider< lotrFilmSeries >
let lotrFilmSeriesProvider   = LotrFilmSeriesProvider.Load( lotrFilmSeries )

(** Table of Budget, Box Office and Running Time **)
let overallFirstTablesRows            = lotrFilmSeriesProvider.Tables.Table1.Rows

// Running Time
let overallRunTime          = overallFirstTablesRows.[ 12 ].``The Lord of the Rings 2``  
let overallRunTimeInMinutes = int( overallRunTime.Split(' ').[ 0 ] )

// Budget
let overallBudget                     = overallFirstTablesRows.[ 15 ].``The Lord of the Rings 2``
let overallBudgetInMillions           = int ( overallBudget.Split(' ').[ 0 ].Replace( "$", "" ))

// Box Office Revenue
let overallBoxOfficeRevenue           = overallFirstTablesRows.[ 16 ].``The Lord of the Rings 2``
let overallBoxOfficeRevenueInMillions = float( overallBoxOfficeRevenue.Split(' ').[ 0 ].Replace( "$", "" )) * 1000.

(** List of Academy Award Nominations vs. Wins  **)
// Fellowship of the Ring
let forAcademyAwardsData  = lotrFilmSeriesProvider.Lists.``Academy Awards``.Values.[ 0 ]
let forNominationsAndWins = forAcademyAwardsData.Split('-').[ 1 ].Split(':') // Urghh, couldn't generalize the algorithm.. :(
let forNominations        = int ( forNominationsAndWins.[ 1 ].Split(',').[ 0 ].Replace( " ", "" ))
let forWins               = int ( forNominationsAndWins.[ 2 ].Replace( " ", "" ))
let forRottenTomatoes     = int ( lotrFilmSeriesProvider.Tables.``Public and critical response``.Rows.[ 0 ].``Rotten Tomatoes``.Split(' ').[0].Replace("%", ""))

// The Two Towers
let ttAcademyAwardsData   = lotrFilmSeriesProvider.Lists.``Academy Awards``.Values.[ 1 ]
let ttNominationsAndWins  = ttAcademyAwardsData.Split('—').[ 1 ].Split(':')
let ttNominations         = getNominationData( ttNominationsAndWins ) 
let ttWins                = getWinsData( ttNominationsAndWins ) 
let ttRottenTomatoes      = int ( lotrFilmSeriesProvider.Tables.``Public and critical response``.Rows.[ 1 ].``Rotten Tomatoes``.Split(' ').[0].Replace("%", ""))

// The Return of the King
let rokAcademyAwardsData  = lotrFilmSeriesProvider.Lists.``Academy Awards``.Values.[ 2 ]
let rokNominationsAndWins = rokAcademyAwardsData.Split('—').[ 1 ].Split(':')
let rokNominations        = getNominationData( rokNominationsAndWins )
let rokWins               = getWinsData( rokNominationsAndWins )
let rokRottenTomatoes     = int ( lotrFilmSeriesProvider.Tables.``Public and critical response``.Rows.[ 2 ].``Rotten Tomatoes``.Split(' ').[0].Replace("%", ""))

// Overall
let overallNominations            = forNominations + ttNominations + rokNominations
let overallWins                   = forWins        + ttWins        + rokWins 
let overallAvgRottenTomatoesScore = ( float ( forRottenTomatoes + ttRottenTomatoes + rokRottenTomatoes )) / 3.0

let overallMovieInfo = { Name                       = "The Lord of the Rings Series";
                         RuntimeInMinutes           = overallRunTimeInMinutes;
                         BudgetInMillions           = overallBudgetInMillions;
                         BoxOfficeRevenueInMillions = overallBoxOfficeRevenueInMillions;
                         AcademyAwardNominations    = overallNominations;
                         AcademyAwardWins           = overallWins; 
                         RottenTomatoesScore        = overallAvgRottenTomatoesScore; }

(* Fellowship of the Ring Specific Data *)

[<Literal>]
let fellowshipOfTheRingWiki      = @"https://en.wikipedia.org/wiki/The_Lord_of_the_Rings:_The_Fellowship_of_the_Ring"
type FellowshipOfTheRingProvider = HtmlProvider< fellowshipOfTheRingWiki > 
let fellowshipOfTheRingProvider  = FellowshipOfTheRingProvider.Load( fellowshipOfTheRingWiki )

(** Running Time, Budget and Box Office Revenue **)
let forFirstTableRows   = fellowshipOfTheRingProvider.Tables.Table1.Rows

let forRuntime          = forFirstTableRows.[ 12 ].``The Lord of the Rings: The Fellowship of the Ring 2``.Split(' ').[ 0 ]
let forRuntimeInMinutes = int ( forRuntime )

let forBudget           = forFirstTableRows.[ 15 ].``The Lord of the Rings: The Fellowship of the Ring 2``.Split(' ').[ 0 ].Replace("$", "")
let forBudgetInMillions = int ( forBudget )

let forBoxOfficeRevenue           = forFirstTableRows.[ 16 ].``The Lord of the Rings: The Fellowship of the Ring 2``.Split(' ').[ 0 ].Replace("$", "")
let forBoxOfficeRevenueInMillions = float ( forBoxOfficeRevenue )

let forMovieInfo =  { Name                       = "The Fellowship of the Ring";
                      BudgetInMillions           = forBudgetInMillions;
                      BoxOfficeRevenueInMillions = forBoxOfficeRevenueInMillions;
                      RuntimeInMinutes           = forRuntimeInMinutes;
                      AcademyAwardNominations    = forNominations;
                      AcademyAwardWins           = forWins; 
                      RottenTomatoesScore        = float( forRottenTomatoes ); }

(* The Two Towers Specific Data *)

[<Literal>]
let twoTowersWiki      = @"https://en.wikipedia.org/wiki/The_Lord_of_the_Rings:_The_Two_Towers"
type TwoTowersProvider = HtmlProvider< twoTowersWiki > 
let twoTowersProvider  = TwoTowersProvider.Load( twoTowersWiki )

(** Running Time, Budget and Box Office Revenue **)
let ttFirstTableRows   = twoTowersProvider.Tables.Table1.Rows

let ttRuntime          = ttFirstTableRows.[ 12 ].``The Lord of the Rings: The Two Towers 2``.Split(' ').[ 0 ]
let ttRuntimeInMinutes = int ( ttRuntime )

let ttBudget           = ttFirstTableRows.[ 15 ].``The Lord of the Rings: The Two Towers 2``.Split(' ').[ 0 ].Replace("$", "")
let ttBudgetInMillions = int ( ttBudget )

let ttBoxOfficeRevenue           = ttFirstTableRows.[ 16 ].``The Lord of the Rings: The Two Towers 2``.Split(' ').[ 0 ].Replace("$", "")
let ttBoxOfficeRevenueInMillions = float ( ttBoxOfficeRevenue )

let ttMovieInfo =  { Name                       = "The Two Towers ";
                     BudgetInMillions           = ttBudgetInMillions;
                     BoxOfficeRevenueInMillions = ttBoxOfficeRevenueInMillions;
                     RuntimeInMinutes           = ttRuntimeInMinutes;
                     AcademyAwardNominations    = ttNominations;
                     AcademyAwardWins           = ttWins; 
                     RottenTomatoesScore        = float( ttRottenTomatoes ); }

(* The Return of the King Specific Data *)

[<Literal>]
let returnOfTheKingWiki      = @"https://en.wikipedia.org/wiki/The_Lord_of_the_Rings:_The_Return_of_the_King"
type ReturnOfTheKingProvider = HtmlProvider< returnOfTheKingWiki >
let returnOfTheKingProvider  = ReturnOfTheKingProvider.Load( returnOfTheKingWiki )

(** Running Time, Budget and Box Office Revenue **)
let rokFirstTableRows   = returnOfTheKingProvider.Tables.Table1.Rows

let rokRuntime          = rokFirstTableRows.[ 12 ].``The Lord of the Rings: The Return of the King 2``.Split(' ').[ 0 ]
let rokRuntimeInMinutes = int ( rokRuntime )

let rokBudget           = rokFirstTableRows.[ 15 ].``The Lord of the Rings: The Return of the King 2``.Split(' ').[ 0 ].Replace("$", "")
let rokBudgetInMillions = int ( rokBudget )

let rokBoxOfficeRevenue           = rokFirstTableRows.[ 16 ].``The Lord of the Rings: The Return of the King 2``.Split(' ').[ 0 ].Replace("$", "")
let rokBoxOfficeRevenueInMillions = float ( rokBoxOfficeRevenue )

let rokMovieInfo =  { Name                       = "The Return of the King";
                      BudgetInMillions           = rokBudgetInMillions;
                      BoxOfficeRevenueInMillions = rokBoxOfficeRevenueInMillions;
                      RuntimeInMinutes           = rokRuntimeInMinutes;
                      AcademyAwardNominations    = rokNominations;
                      AcademyAwardWins           = rokWins; 
                      RottenTomatoesScore        = float( rokRottenTomatoes ); }

let allMoviesInfo  =
    [
        overallMovieInfo;
        forMovieInfo;
        ttMovieInfo;
        rokMovieInfo
    ]
  
let allMoviesCsv = 
    allMoviesInfo
    |> List.map( MovieInfo.ToCsv )