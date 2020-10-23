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
            time.sleep(0.2)
            self.currentMetrics = {
                'epoch': i,
                'accuracy': i * 0.2 + (0.2 * rn.random()),
                'validationAccuracy': i * 0.18 + (0.15 * rn.random()),
            }
            self.progress(self.currentMetrics)

        self.complete({
            'jobId': self.jobData['id'],
            'metrics': self.currentMetrics
        }, self.deliveryTag)
