from typing import Any, Callable
import time
import random as rn


class TrainWorker:

    def __init__(self,
                 jobData: dict,
                 progress: Callable[[dict], None],
                 complete: Callable[[dict, int], None],
                 delivery_tag: int) -> None:
        self.jobData = jobData
        self.progress = progress
        self.complete = complete
        self.deliveryTag = delivery_tag

        self.currentMetrics = None

    def train(self) -> None:
        for i in range(0, 5):
            time.sleep(0.8)
            self.currentMetrics = {
                'JobId': self.jobData['Id'],
                'Epoch': i,
                'Accuracy': i * 0.2 + (0.2 * rn.random()),
                'ValidationAccuracy': i * 0.18 + (0.15 * rn.random()),
            }
            self.progress(self.currentMetrics)

        self.complete({
            'NeuralJob': self.jobData,
            'Metrics': self.currentMetrics
        }, self.deliveryTag)
