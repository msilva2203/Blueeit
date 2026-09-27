import "./BlockItem.css";

export default function BlockItem({children}) {
    return (
        <>
            <div className="blueeit-block-item">
                {children}
            </div>
        </>
    );
}