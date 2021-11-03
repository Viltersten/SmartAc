# Theorem Practical Exercise - Proof of Concept for SmartAC

Please read these documents to begin the project:

* [Introduction](docs/project/smartac-intro.md)
* [SmartAC specification](docs/project/smartac-spec.md)
* Prioritization:
  * for [Backend-only variation](docs/project/smartac-priorities-backend-only.md)
  * for [Fullstack variation](docs/project/smartac-priorities-fullstack.md)

Add any additional project documentation here as you work on the project and complete the assigned tasks.

How to start?
Press F5 in VS. The connection string needs to be specified. It's dependent on your specific environment. In my case, I'm using default Sql Express and the string reads "Server=.\\SQLEXPRESS;Database=Theorem;Uid=sa;Pwd=Abc123()".

I'm choosing not to go dockerize. The reason is simplicity. It's additional complexity which can be omitted for the moment. There's nothing preventing us from creating a deployment and YAML'ize it at a later stage. The only consideration is to keep in mind that there's need to introduce a distributed cache for session control for multi-pod environment. Also, I'm applying the pattern of private props and explicit types following the latest features of the language.

I'd go with IDS4 but discovered during the assignment that the security may be set up in a basic (and not best-practice) manner. There's no certificate, claims, policies or scopes introduced. The password for any user is "HakunaMatata", alternatively the combo "admin/pass". For mocked devices, th eIDs and secrets are declared in a seeder and applied during migration in empty DB.

The timestamps for events are suffixed by "on" and consist of: recorded (occasion of the value sensed by the device), reported (occasion of the value stored in DB), recognized (occasion of analysis) and resolved (occasion of alert update in DB). By default I rely on GUIDs for any and all entities. It's always sufficient in the long run and normalized, in contrast to strings and integers. I choose not to suffix asynchronous method nor prefix private properties. Naming should reflect purpose not declaration details (as those are rather obvious anyway).

I'm choosing to go with scoped for all the services for now. Optimization of the scope and life time can be done at a later stage when the basic functionality is working and tested. Mocking libraries will be skipped for now and can be introduced at a later stage. The same goes for the controllers. It should be there but for now, I rely on the seeded data.

I'm omitting the automapping for now. The reason is that it usually leades to a pain of configurations and can be done in batch for all the objects once we see how extensive and complicated converstion paradigm is required. Doing it bitwise will cost more time. Data model is structured after usage and necessary conversion declared as extension methods. By default I'm choosing to use int/double although short/float might be more specialised. In reality it tends to create more headache developing than gain optimizing. Premature optimization is evil. I've removed logging for now. It can be wired in later as the complexity of the assignemnt grows.

More details and refined structure of exceptions would allow for more informative feed-back on how to resolve potential issues. It's being skipped for now. The textual alert is requested to be stored. In my opinion, it's a poor design. I would recommmend that we store a code for the event. That way, we save space, we allow for multi-lingual feed-back and still can generated the text by read-only properties. That said, requirement is king. 

The chunking up based on time intervals is unoptimized. Also, filtering algorithm for alerts is a hairy story. Those should be refactored and I'm not entirely proud of those. Primary goal is to make thing work, though. I'd like to experiment with a dedicated class for information about paging and time intervals for concise syntax.







