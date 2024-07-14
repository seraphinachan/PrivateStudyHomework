// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;

// 문자 전송을 위한 인터페이스를 정의
public interface IMessageSender
{
    void SendMessage(string message);
}

// 카카오와 라인 구현 클래스
public class KakaoMessageSender : IMessageSender
{
    public void SendMessage(string message)
    {
        Console.WriteLine("카카오로 문자보냄: " + message);
    }
}

public class LineMessageSender : IMessageSender
{
    public void SendMessage(string message)
    {
        Console.WriteLine("라인으로 문자보냄: " + message);
    }
}

// ShoppingMall 클래스
// 메시지 전송 방식을 주입받아 사용하는 쇼핑몰 클래스를 작성
public class ShoppingMall
{
    private readonly IMessageSender _messageSender;

    // 생성자에서 IMessageSender를 주입받는다
    public ShoppingMall(IMessageSender messageSender)
    {
        _messageSender = messageSender;
    }

    public void Purchase(string invoiceNumber)
    {
        // 송장 번호를 포함한 문자 메시지 전송
        _messageSender.SendMessage("송장 번호: " + invoiceNumber);
    }
}

// 의존성 주입 및 사용
// 카카오에서 라인으로 변경하려면 인스턴스를 변경해서 주입한다
public class Program
{
    public static void Main(string[] args)
    {
        // 카카오로 메시지를 보내도록 설정
        IMessageSender kakaoSender = new KakaoMessageSender();
        ShoppingMall mallWithKakao = new ShoppingMall(kakaoSender);
        mallWithKakao.Purchase("123456789");

        // 라인으로 메시지를 보내도록 변경
        IMessageSender lineSender = new LineMessageSender();
        ShoppingMall mallWithLine = new ShoppingMall(lineSender);
        mallWithLine.Purchase("987654321");
    }
}