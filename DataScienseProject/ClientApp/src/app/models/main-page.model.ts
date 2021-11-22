import { ExecutorModel } from "./executor.model"
import { LatoutStyleModel } from "./latout-style.model"
import { LayoutDataModel } from "./layout-data.model"
import { ProjectTypeModel } from "./project-type.model";
import { TehnologyModel } from "./tehnology.model";

export class MainPageModel {
  projectTypeModel: ProjectTypeModel;
  executorModel: ExecutorModel;
  tehnologyModels: Array<TehnologyModel>;
  layoutDataModels: Array<LayoutDataModel>;
  latoutStyleModels: Array<LatoutStyleModel>;
}
