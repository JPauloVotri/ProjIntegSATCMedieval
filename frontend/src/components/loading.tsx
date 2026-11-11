
import { OrbitProgress } from "react-loading-indicators";

export default function Loading() {
  return (
    <div className="loading-container">
      <OrbitProgress
        color="#FAC800"
        size="medium"
      />
    </div>
  );
}
