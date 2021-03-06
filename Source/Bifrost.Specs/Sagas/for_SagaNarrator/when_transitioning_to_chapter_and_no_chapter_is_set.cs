﻿using Bifrost.Fakes.Sagas;
using Bifrost.Sagas;
using Machine.Specifications;

namespace Bifrost.Specs.Sagas.for_SagaNarrator
{
    [Subject(typeof(SagaNarrator))]
	public class when_transitioning_to_chapter_and_no_chapter_is_set : given.a_saga_narrator
	{
		static Saga saga;
		static TransitionalChapter chapter;

		Establish context = () =>
								{
									saga = new Saga();
									chapter = new TransitionalChapter();

									service_locator_mock.Setup(s => s.GetInstance<TransitionalChapter>()).Returns(chapter);
								};

		Because of = () => narrator.TransitionTo<TransitionalChapter>(saga);

		It should_get_an_instance_of_chapter = () => service_locator_mock.Verify(s => s.GetInstance<TransitionalChapter>());
		It should_set_current_chapter_to_instance = () => saga.CurrentChapter.ShouldEqual(chapter);
        It should_call_on_transitioned_to_on_the_chapter = () => chapter.OnTransitionedToWasCalled.ShouldBeTrue();
	}
}