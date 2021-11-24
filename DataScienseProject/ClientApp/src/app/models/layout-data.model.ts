import { LayoutStyleModel } from "./layout-style.model";

export class LayoutDataModel{
  elementName: string;
  value: string;
  path: string;
  valueText: string;
  elementTypeName: string;
  orderNumber?: number;
  isShowElementName?: boolean;
  layoutStyleModel: Array<LayoutStyleModel>;
}
