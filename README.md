# Theorem Practical Exercise - Proof of Concept for SmartAC

Please read these documents to begin the project:

* [Introduction](docs/project/smartac-intro.md)
* [SmartAC specification](docs/project/smartac-spec.md)
* Prioritization:
  * for [Backend-only variation](docs/project/smartac-priorities-backend-only.md)
  * for [Fullstack variation](docs/project/smartac-priorities-fullstack.md)

Add any additional project documentation here as you work on the project and complete the assigned tasks.

I'm choosing not to go dockerize. Reason is simplicity. It's additional complexity which can be omitted for the moment. There's nothing preventing us from creating a deployment and YAML'ize it at a later stage. The only consideration is to keep in mind that there's need to be a distributed cache for session control for multi-pod environment.

I'll go with IDS4 but skip the cetificate for now. Dev creds work fine and I want to keep the complexity to a minimum at the moment. I'm considering splitting the IDP and the Api controllers in separate projects because of a microservice architecutre.

I'm removing logging for now. It can be wired in later as the complexity of the assignemnt grows.

I'm choosing to go with scoped for all the services for now. Optimization of the scope and life time can be done at a later stage when the basic functionality is working and tested.

I'm skipping mocking libraries for the moment. I can mock up services at a later stage. The same goes for the controllers. It needs to be there but I want to have something running soon to bring down the anxiety. 

i'm omitting the automapper for now. The reason is that it usually leades to a pain of configurations and can be done in chunk for all the objects once I see how extensive and complicated converstion paradigm is required. Doing it bitwise will cost more time.








