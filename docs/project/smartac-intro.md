# Theorem Practical Exercise - Proof of Concept for SmartAC

## Introduction

This is a fictional project for a fictional client, but please treat the project in a realistic fashion including any communication with the team.  

**LICENSE: _All resources included in this project, and communication via Slack are to be treated as confidential and cannot be shared publicly outside of this exercise; either in their original form or as derivations.  You may retain a private copy for your own use only._**

## Situational Summary

One of our clients is considering working with us on a large future project.  They have given us a proof of concept (PoC) to implement that has two goals:

* Show the key concepts of the project to their internal project funding board.
* See how we build code that meets their requirements.

These goals are in _conflict with each other_ as a PoC or prototype typically favors features and showcasing concepts over code quality.  But at Theorem, we never sacrifice code quality.  Instead we make clear decisions about scope and simplifying the features to gain the speed of development.  Therefore our product manager has worked with the client to narrow down their scope to the bare minimum, with some optional features that we can build if time allows.  

As a tip, if you take any shortcuts, please document them in the README so that it is obvious what was intentionally done in this regard.

### The client is expecting:

* Progress on important features.
* Solid code, even if simplified for the PoC.  Avoid over engineering.
* At least 1 test case showing how we test backend REST endpoints (the HOW is more important than quantity here).  You do not need to go for 100% coverage, prioritize tests below features, but still have something to show for testing.
* Notes about shortcuts taken during the PoC that were intentionally done, to help them review the quality of our work without taking into account intentional shortcuts as negatives.
* Documentation for the backend API's (Swagger preferred, but Postman file, or Apiary, or other documentation is allowed).  If not deploying, it is helpful to have a dump of the API documents that can be viewed offline.
* The ability to run the application simply, preferably with one command.  (i.e. docker compose, a deployed app in the cloud, or one click in a typical IDE).  Sometimes using in memory, or embedded databases helps with this, but make sure they could easily be upgraded to a full database later without many code changes.

### How you should work:

* Ask questions in the Slack channel if you have concerns or need clarification for features.  Continue to work on other items as replies will be asynchronous.
* Provide updates to the slack channel how you might normally give a written standup.  It is good that the team knows where things are headed and progress that has been made.  Feel free to use text, images, screenshots, or even videos if that helps your communication.
* Commit to Git as you normally would on a team project.  You may also use Pull Requests (PR) -- but for this exercise please just immediately merge them yourself instead of waiting on reviews or a teammember to do so.

### How to prioritize your work:

This exercise has two variations that use the same [project specification for the SmartAC application](smartac-spec.md) but have different priorities:

* [Backend Only - scope and priorities](smartac-priorities-backend-only.md) - for developers who are only completing backend tasks.
* [Fullstack - scope and prioritie](smartac-priorities-fullstack.md) - for developers completing a mix of backend and frontend tasks.

Please read the section according to your role and the instructions given to you when invited to this repository.

## Language / Framework tips

In general there are also tips for different languages/frameworks that you should consider ahead of developing this proof of concept application.

**If you are a C# / .NET developer:**
* Use C# / .NET 5 or newer.
* It is preferred that you use Entity Framework for data access even if with an in-memory database (a real database preferred).  If you choose something else, please substantiate the decision in the README and be sure you use the other framework with best practices in mind. In case you go for a DB that is **not** supported by Entity Framework, such as NoSQL, disregard the previous suggestion.
* For UI, using a SPA framework such as React or Angular is fine.  If instead you use a server-side framework such as Razor, please call the backend REST API instead of including all of the logic in Razor pages.

**If you are a Ruby developer:**
* Use current Ruby and note the version clearly.
* It is preffered that you do not use Rails for the backend API, use an alternative framework.  
* Any UI (React, Angular, ... whatever) should call the backend API instead of rendering everything via server-side templates.

**If you are a JavaScript / TypeScript developer:**
* Include `.nvmrc`  `node-version` or similar file in the project indicating the required Node version.
* Use TypeScript in Node.js or Deno for the backend API.
* Structure your backend cleanly, even if it is a basic express app.  You may also consider backend frameworks such as TS.ed, Nest, Foal, or similar. 
* Use TypeScript in your frontend UI code whether that is with React, Angular, Vue, or other framework.

**If you are a Python developer:**
* Use Python 3.8 or newer
* Use Flask or no framework when developing backend
* If you are doing full stack development, use a distinct separation between the API and the frontend code.

**If you are a Java developer:**
* Use Gradle, and include the minimum JVM version as appropriate settings in your build file.
* Use Java 11 or newer, preferably Java 17 which is now the LTS version.
* Use a backend framework to create an API using your favorite framework such as Spring Boot, Quarkus, Micronaut, Vert.x, or similar.
* Any UI (React, Angular, ... whatever) should call the backend API instead of rendering everything via server-side templates (JSP or similar).

**If you are a Kotlin developer:**
* See same tips as for Java, and use the most currently released Kotlin version.
* You can use Kotlin specific frameworks such as KTOR, Kotlinx Serialization, and any others you deem appropriate.
