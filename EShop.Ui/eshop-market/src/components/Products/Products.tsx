import React from "react";
import {
  Card,
  DataGrid,
  DataGridBody,
  DataGridCell,
  DataGridHeader,
  DataGridHeaderCell,
  DataGridRow,
  TableColumnDefinition,
  createTableColumn,
} from "@fluentui/react-components";

import { Product } from "../../models/Product";

const columns: TableColumnDefinition<Product>[] = [
  createTableColumn<Product>({
    columnId: "productName",
    renderHeaderCell: () => {
      return "Название";
    },
    renderCell: (item) => {
      return item.name;
    },
  }),
  createTableColumn<Product>({
    columnId: "productPrice",
    renderHeaderCell: () => {
      return "Цена без скидки";
    },
    renderCell: (item) => {
      return item.price;
    },
  }),
  createTableColumn<Product>({
    columnId: "productSalePrice",
    renderHeaderCell: () => {
      return "Цена со скидкой";
    },
    renderCell: (item) => {
      return item.salePrice;
    },
  }),
];

type ProductsProps = {
  products: Product[];
  onProductSelect?: () => void;
};

const Products: React.FunctionComponent<ProductsProps> = ({
  products,
  onProductSelect,
}) => {
  return (
    <Card>
      <DataGrid
        items={products}
        columns={columns}
        getRowId={(item) => item.id}
        selectionMode="single"
      >
        <DataGridHeader>
          <DataGridRow>
            {({ renderHeaderCell }) => (
              <DataGridHeaderCell>{renderHeaderCell()}</DataGridHeaderCell>
            )}
          </DataGridRow>
        </DataGridHeader>
        <DataGridBody<Product>>
          {({ item, rowId }) => (
            <DataGridRow<Product> key={rowId}>
              {({ renderCell }) => (
                <DataGridCell>{renderCell(item)}</DataGridCell>
              )}
            </DataGridRow>
          )}
        </DataGridBody>
      </DataGrid>
    </Card>
  );
};

export { Products };
