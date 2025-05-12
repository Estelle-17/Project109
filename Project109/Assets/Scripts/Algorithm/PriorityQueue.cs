using System;
using UnityEngine;

public class PriorityQueue<T> where T : IComparable<T>
{
    T[] data;
    public int Count { get; private set; }
    public int Capacity { get; private set; }

    #region ������
    public PriorityQueue()
    {
        Count = 0;
        Capacity = 1;
        data = new T[Capacity];
    }

    public PriorityQueue(int capacity)
    {
        Count = 0;
        Capacity = capacity;
        data = new T[Capacity];
    }
    #endregion

    public void Enqueue(T value)
    {
        if(Count >= Capacity)
        {
            Expand();
        }
        data[Count] = value;
        Count++;

        //Ʈ�� ����� �������� ������ ���� ������ �̵�
        int currentDataIndex = Count - 1;
        while(currentDataIndex > 0)
        {
            int parentDataIndex = (currentDataIndex - 1) / Capacity;

            //�θ� ����� ���� �� ũ�ų� ���ٸ� ����
            if (data[currentDataIndex].CompareTo(data[parentDataIndex]) > 0)
                break;

            //���� ��� ���� ��ȯ
            T temp = data[currentDataIndex];
            data[currentDataIndex] = data[parentDataIndex];
            data[parentDataIndex] = temp;

            currentDataIndex = parentDataIndex;
        }
    }

    public T Dequeue()
    {
        if(Count == 0)
            throw new IndexOutOfRangeException();

        //���� ó�� �� ����
        T result = data[0];
        data[0] = data[Count - 1];
        data[Count - 1] = default(T);
        Count--;

        int currentDataIndex = 0;
        while(currentDataIndex < Count) 
        {
            int left = (currentDataIndex * 2) + 1;
            int right = (currentDataIndex * 2) + 2;

            int nextDataIndex = currentDataIndex;

            //����, ������ ���� �� �� ���� ���� �����Ѵٸ� nextData ����
            //���ٸ� ������ �ʿ䰡 ������ while�� ����
            if(left < Count && data[currentDataIndex].CompareTo(data[left]) > 0)
                nextDataIndex = left;
            if (right < Count && data[currentDataIndex].CompareTo(data[right]) > 0)
                nextDataIndex = right;
            if (nextDataIndex == currentDataIndex)
                break;

            //�� ��ȯ
            T temp = data[currentDataIndex];
            data[currentDataIndex] = data[nextDataIndex];
            data[nextDataIndex] = temp;

            currentDataIndex = nextDataIndex;
        }

        return result;
    }

    public T Peek()
    {
        if(Count == 0)
            throw new IndexOutOfRangeException();

        return data[0];
    }

    void Expand()
    {
        T[] newData = new T[Capacity * 2];
        for(int i = 0; i < Count; i++)
        {
            newData[i] = data[i];
        }
        
        data = newData;
        Capacity *= 2;
    }
}
