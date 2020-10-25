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

        self.currentMetric = None
        self.metrics = []

    def train(self) -> None:        
        for i in range(0, 5):
            time.sleep(0.8)
            self.currentMetric = {
                'JobId': self.jobData['Id'],
                'EpochDuration': rn.randint(1000, 10000),
                'Epoch': i,
                'Accuracy': i * 0.2 + (0.2 * rn.random()),
                'ValidationAccuracy': i * 0.18 + (0.15 * rn.random()),
                'Loss': i * 0.2 + (0.2 * rn.random()),
                'ValidationLoss': i * 0.18 + (0.15 * rn.random()),
            }            
            self.metrics.append(self.currentMetric)
            self.progress(self.currentMetric)

        self.complete({
            'JobId': self.jobData['Id'],
            'Metrics': self.metrics
        }, self.deliveryTag)
