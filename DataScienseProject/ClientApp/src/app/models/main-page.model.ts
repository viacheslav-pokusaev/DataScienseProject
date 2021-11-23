import { ExecutorModel } from "./executor.model"
import { LayoutStyleModel } from "./layout-style.model"
import { LayoutDataModel } from "./layout-data.model"
import { ProjectTypeModel } from "./project-type.model";
import { TehnologyModel } from "./tehnology.model";

export class MainPageModel {
  projectTypeModels: Array<ProjectTypeModel>;
  executorModels: Array<ExecutorModel>;
  tehnologyModels: Array<TehnologyModel>;
  layoutDataModels: Array<LayoutDataModel>;
  layoutStyleModels: Array<LayoutStyleModel>;
}
