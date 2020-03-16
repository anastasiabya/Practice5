using System;

namespace Korop_AI_5
{
    class Program
    {
        /// <summary>
        /// Точка входа программы
        /// </summary>
        static void Main()
        {
            ManagerMediator mediator = new ManagerMediator();
            Colleague consumer = new ConsumerColleague(mediator);
            Colleague restaurant = new RestaurantColleague(mediator);
            Colleague deliveryman = new DeliverymanColleague(mediator);
            mediator.Consumer = consumer;
            mediator.Restaurant = restaurant;
            mediator.Deliveryman = deliveryman;
            consumer.Send("Есть заказ, надо приготовить");
            restaurant.Send("Заказ готов, надо доставить");
            deliveryman.Send("Заказ доставлен, надо забрать");

            Console.Read();
        }
    }

    /// <summary>
    /// Интерфейс для взаимодействия с объектами Colleague
    /// </summary>
    abstract class Mediator
    {
        public abstract void Send(string msg, Colleague colleague);
    }

    /// <summary>
    /// Интерфейс для взаимодействия с объектом Mediator
    /// </summary>
    abstract class Colleague
    {
        protected Mediator mediator;

        public Colleague(Mediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Метод определения получателя
        /// </summary>
        /// <param name="message">Сообщение</param>
        public virtual void Send(string message)
        {
            mediator.Send(message, this);
        }

        /// <summary>
        /// Метод отправки сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        public abstract void Notify(string message);
    }

    
    /// <summary>
    /// Один из конкретных классов, которые обменивается друг с другом через объект Mediator
    /// Класс потребителя/заказчика
    /// </summary>
    class ConsumerColleague : Colleague
    {
        public ConsumerColleague(Mediator mediator)
            : base(mediator)
        {
        }

        /// <summary>
        /// Метод отправки сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        public override void Notify(string message)
        {
            Console.WriteLine("Сообщение заказчику: " + message);
        }
    }

    
    /// <summary>
    /// Один из конкретных классов, которые обменивается друг с другом через объект Mediator
    /// Класс ресторана
    /// </summary>
    class RestaurantColleague : Colleague
    {
        public RestaurantColleague(Mediator mediator)
            : base(mediator)
        {
        }

        /// <summary>
        /// Метод отправки сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        public override void Notify(string message)
        {
            Console.WriteLine("Сообщение ресторану: " + message);
        }
    }

    
    /// <summary>
    /// Один из конкретных классов, которые обменивается друг с другом через объект Mediator
    /// Класс доставщика
    /// </summary>
    class DeliverymanColleague : Colleague
    {
        public DeliverymanColleague(Mediator mediator)
            : base(mediator)
        {
        }

        /// <summary>
        /// Метод отправки сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        public override void Notify(string message)
        {
            Console.WriteLine("Сообщение доставщику: " + message);
        }
    }

    
    /// <summary>
    /// Конкретный посредник, реализующий интерфейс типа Mediator
    /// </summary>
    class ManagerMediator : Mediator
    {
        public Colleague Consumer { get; set; }
        public Colleague Restaurant { get; set; }
        public Colleague Deliveryman { get; set; }

        /// <summary>
        /// Проверяет, от кого пришло сообщение, и в зависимости от отправителя перенаправляет его другому объекту
        /// </summary>
        /// <param name="msg">Сообщение</param>
        /// <param name="colleague">Один из конкретных классов, от которого пришло сообщение</param>
        public override void Send(string msg, Colleague colleague)
        {
            if (Consumer == colleague)
                Restaurant.Notify(msg);
            else if (Restaurant == colleague)
                Deliveryman.Notify(msg);
            else if (Deliveryman == colleague)
                Consumer.Notify(msg);
        }
    }
}