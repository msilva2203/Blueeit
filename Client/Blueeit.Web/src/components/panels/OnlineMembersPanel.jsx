import Block from "../Block";
import BlockContent from "../BlockContent";
import BlockFooter from "../BlockFooter";

import "./OnlineMembersPanel.css";
import "./Panel.css";

export default function OnlineMembersPanel() {
    return (
        <Block>
            <h3 className="blueeit-panel-header">Members Online</h3>
            <div className="blueeit-panel-body placeholder">
                
            </div>
            <BlockFooter>
                <BlockContent>
                    <small>Total: 0</small>
                </BlockContent>
            </BlockFooter>
        </Block>
    );
}