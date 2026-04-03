# Chess Online - Project Context

## About
Online chess web app (pet project for learning web development).

## Developer
C# beginner, first web project. Knows C# basics only. Goal: learn web dev deeply, not just get working code.

## Tech Stack
- ASP.NET Core 8 (Web API + SignalR)
- Blazor WebAssembly (frontend)
- Entity Framework Core + SQLite
- xUnit for tests
- JWT authentication

## Solution Structure
- ChessOnline.Server - backend API, SignalR hubs, EF Core
- ChessOnline.Client - Blazor WASM frontend
- ChessOnline.Shared - DTOs shared between server and client
- ChessOnline.Engine - chess rules, move validation, bot AI
- ChessOnline.Tests - unit tests

## Key Features
- Online matchmaking (blitz only: 3+2 or 5+0)
- Guest mode (24h temporary accounts, no registration needed, upgradeable to permanent)
- Bot play (minimax AI, multiple difficulty levels)
- Friends system (add by nickname/ID, invite to games)
- Game history and Elo rating stats

## Work Format
- Give small tech-tasks for key logic (models, services, LINQ queries, validators)
- Developer writes code, Claude reviews and explains
- Claude handles boilerplate/config (Program.cs, .csproj, migrations, CSS)
- Always explain WHY something is done, not just WHAT
- Reference PDF learning materials (Parts 1-4) when relevant concepts appear

## Current Stage
Stage 1 - Foundation: Solution setup, DB models, registration, login, guest mode.

## Architecture Decisions
- Server validates ALL moves (anti-cheat)
- SignalR for real-time (moves, matchmaking), REST for everything else
- Guest users: IsGuest=true, GuestExpiresAt, nullable Email/PasswordHash
- Background service cleans expired guests every hour
