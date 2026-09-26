using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioReactiveSpeaker : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public int sampleDataLength = 1024; // Количество выборок для анализа (лучше 256, 512, 1024)

    [Header("Scale Settings")]
    public Vector3 baseScale = new Vector3(1f, 1f, 1f); // Исходный размер динамика
    public float scaleMultiplier = 2f;                  // Насколько сильно динамик реагирует на бас
    public float smoothSpeed = 15f;                     // Плавность сжатия/возврата в исходное состояние

    private float[] clipSampleData;

    void Start()
    {
        // Если AudioSource не назначен вручную, берем его с этого же объекта
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        clipSampleData = new float[sampleDataLength];
    }

    void Update()
    {
        if (audioSource == null || !audioSource.isPlaying)
        {
            // Плавное возвращение к исходному размеру, если музыка не играет
            transform.localScale = Vector3.Lerp(transform.localScale, baseScale, Time.deltaTime * smoothSpeed);
            return;
        }

        // Получаем данные о текущей звуковой волне
        audioSource.GetOutputData(clipSampleData, 0);

        // Считаем среднюю громкость (RMS - Root Mean Square)
        float sum = 0f;
        for (int i = 0; i < sampleDataLength; i++)
        {
            sum += clipSampleData[i] * clipSampleData[i];
        }

        float rmsValue = Mathf.Sqrt(sum / sampleDataLength);

        // Вычисляем целевой масштаб на основе громкости
        float targetScaleValue = baseScale.x + (rmsValue * scaleMultiplier);
        Vector3 targetScale = new Vector3(targetScaleValue, targetScaleValue, targetScaleValue);

        // Применяем сглаживание, чтобы динамик не дергался слишком резко
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * smoothSpeed);
    }
}
