import "./BlockContent.css";

export default function BlockContent({children}) {
    return (
        <div className="blueeit-block-content">
            {children}
        </div>
    );
}