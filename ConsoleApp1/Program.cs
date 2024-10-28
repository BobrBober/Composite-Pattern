using System;
using System.Collections.Generic;

// Интерфейс для компонентов документа
public interface IDocumentComponent
{
    void Add(IDocumentComponent component);
    void Remove(IDocumentComponent component);
    void Display(int indent);
}

// Класс для представления параграфа
public class Paragraph : IDocumentComponent
{
    private string text;

    public Paragraph(string text)
    {
        this.text = text;
    }

    public void Add(IDocumentComponent component)
    {
        throw new InvalidOperationException("Нельзя добавлять в параграф.");
    }

    public void Remove(IDocumentComponent component)
    {
        throw new InvalidOperationException("Нельзя удалять из параграфа.");
    }

    public void Display(int indent)
    {
        Console.WriteLine(new string(' ', indent) + text);
    }
}

// Класс для представления раздела
public class Section : IDocumentComponent
{
    private string title;
    private List<IDocumentComponent> components = new List<IDocumentComponent>();

    public Section(string title)
    {
        this.title = title;
    }

    public void Add(IDocumentComponent component)
    {
        components.Add(component);
    }

    public void Remove(IDocumentComponent component)
    {
        components.Remove(component);
    }

    public void Display(int indent)
    {
        Console.WriteLine(new string(' ', indent) + title);
        foreach (var component in components)
        {
            component.Display(indent + 2); // Увеличиваем отступ для вложенных элементов
        }
    }
}

// Класс для представления документа
public class Document : IDocumentComponent
{
    private List<IDocumentComponent> sections = new List<IDocumentComponent>();

    public void Add(IDocumentComponent component)
    {
        sections.Add(component);
    }

    public void Remove(IDocumentComponent component)
    {
        sections.Remove(component);
    }

    public void Display(int indent)
    {
        Console.WriteLine("Документ:");
        foreach (var section in sections)
        {
            section.Display(indent + 2); // Увеличиваем отступ для секций
        }
    }
}

// Пример использования
public class Program
{
    public static void Main()
    {
        // Создание документа
        Document document = new Document();

        // Создание секций
        Section section1 = new Section("Введение");
        section1.Add(new Paragraph("Это параграф введения. В этом разделе мы обсудим основные цели документа и его структуру."));
        section1.Add(new Paragraph("Документ состоит из нескольких разделов, которые подробно освещают тему."));

        Section section2 = new Section("Основное содержание");
        section2.Add(new Paragraph("Это параграф основного содержания. Здесь будет представлена основная информация по теме."));

        Section subsection = new Section("Подраздел");
        subsection.Add(new Paragraph("Это параграф в подразделе. В данном разделе будут приведены примеры и пояснения."));
        subsection.Add(new Paragraph("Примеры помогут лучше понять рассматриваемые вопросы и их важность."));

        section2.Add(subsection);
        section2.Add(new Paragraph("Кроме того, мы рассмотрим различные аспекты, связанные с темой."));

        document.Add(section1);
        document.Add(section2);

        // Отображение структуры документа
        document.Display(0);
    }
}
