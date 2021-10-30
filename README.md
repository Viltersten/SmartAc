# Theorem Practical Exercise - Proof of Concept for SmartAC

Please read these documents to begin the project:

* [Introduction](docs/project/smartac-intro.md)
* [SmartAC specification](docs/project/smartac-spec.md)
* Prioritization:
  * for [Backend-only variation](docs/project/smartac-priorities-backend-only.md)
  * for [Fullstack variation](docs/project/smartac-priorities-fullstack.md)

Add any additional project documentation here as you work on the project and complete the assigned tasks.

I'm choosing not to go dockerize. Reason is simplicity. It's additional complexity which can be omitted for the moment. There's nothing preventing us from creating a deployment and YAML'ize it at a later stage. The only consideration is to keep in mind that there's need to be a distributed cache for session control for multi-pod environment. Also, I'm applying hte pattern of private props and explicit types.

I'll go with IDS4 but skip the cetificate for now. Dev creds work fine and I want to keep the complexity to a minimum at the moment. I'm considering splitting the IDP and the Api controllers in separate projects because of a microservice architecutre.

The R-occasions for events are prefixed by "on" and consist of: recorder, reported, recognized and resolved. By default I rely on GUIDs for any and all entities. It's a bit clunkier than straight strings or int. But it always suffice in the long run. I choose not to suffix asynchronous method nor prefix private properties in order not to clog the code, since it's rather obvious anyway.

I'm removing logging for now. It can be wired in later as the complexity of the assignemnt grows.

I'm choosing to go with scoped for all the services for now. Optimization of the scope and life time can be done at a later stage when the basic functionality is working and tested.

I'm skipping mocking libraries for the moment. I can mock up services at a later stage. The same goes for the controllers. It needs to be there but I want to have something running soon to bring down the anxiety. 

I'm omitting the automapper for now. The reason is that it usually leades to a pain of configurations and can be done in chunk for all the objects once I see how extensive and complicated converstion paradigm is required. Doing it bitwise will cost more time.

I'm choosing to use int/double although short/float might be more specialised. In reality it makes more issue developing than it creates gain optimizing. Premature optimization is evil.

More details and refined structure of exceptions would allow for more informative fee-back on how to resolve potential issues. It's being skipped for now.

The textual alert is requested to be stored. In my opinion, it's a poor design. I would recommmend that we store a code for the event. That way, we save space, we allow for multi-lingual feed-back and still can generated the text by read-only properties. THat said, requirement is king. On the same note, being in state "resolved" or "ignored", implies a decision made, hence implying state of "viewed". Optimally, I'd suggest unifying those into four stages: new, viewed, resolved, ignored.






