import { ExecutorModel } from "../executor.model"

export class GalleryModel {
  viewKey: number;
  viewName: string;
  image: string;
  executors: Array<ExecutorModel>;
  tags: Array<string>;
  shortDescription: Array<string>;
  orderNumber?: number;  
}
